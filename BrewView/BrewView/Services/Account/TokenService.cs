using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrewView.Http;
using BrewView.Models;
using Xamarin.Essentials;

namespace BrewView.Services.Account
{
    public class TokenService : ITokenService
    {
        private readonly IRestClient m_restClient;
        private string m_idToken;
        private string m_refreshToken;
        private string m_state;
        private string m_verifier;

        public TokenService(IRestClient restClient)
        {
            m_restClient = restClient;
        }

        private bool HasTokens => !string.IsNullOrEmpty(IdToken) || !string.IsNullOrEmpty(RefreshToken);

        public string IdToken
        {
            get => m_idToken ?? Preferences.Get(AppConstants.IdToken, string.Empty);
            set
            {
                m_idToken = value;
                Preferences.Set(AppConstants.IdToken, value);
            }
        }

        public string RefreshToken
        {
            get => m_refreshToken ?? Preferences.Get(AppConstants.RefreshToken, string.Empty);
            set
            {
                m_refreshToken = value;
                Preferences.Set(AppConstants.RefreshToken, value);
            }
        }

        public string AuthenticationProvider => Preferences.Get(AppConstants.TokenIssuer, string.Empty).ToLower();

        public async Task<bool> HasValidTokens()
        {
            if (!HasTokens) return false;
            var handler = new JwtSecurityTokenHandler();
            var id = handler.ReadJwtToken(IdToken);
            return id.ValidTo > DateTime.UtcNow.AddSeconds(5) || await IsRefreshTokenValid(RefreshToken);
        }

        public (string IdToken, string RefreshToken) GetTokens()
        {
            return (IdToken, RefreshToken);
        }

        public async Task ProviderSignIn(string provider)
        {
            m_verifier = PKCE.GenerateSecureString();
            m_state = PKCE.GenerateSecureString();

            Preferences.Set(AppConstants.TokenIssuer, provider);

            await Browser.OpenAsync(
                $"{AppConstants.BaseAddress}/{AppConstants.AuthenticationRequestEndpoint}{provider}?codeChallenge={PKCE.GenerateCodeChallenge(m_verifier)}&state={m_state}",
                BrowserLaunchMode.SystemPreferred);
        }

        public async Task<bool> ProviderTokenRequest(string path)
        {
            var code = VerifyRedirect(path);

            if (string.IsNullOrEmpty(code)) return false;

            try
            {
                var httpRequestMessage = new HttpRequestBuilder().WithMethod(HttpMethod.Get)
                    .WithRequestUri($"{AppConstants.TokenRequestEndpoint}{AuthenticationProvider}")
                    .AddQueryParameter("code", code)
                    .AddQueryParameter("codeVerifier", m_verifier)
                    .Build();

                var response = await m_restClient.Send<UserValidationResponse>(httpRequestMessage);

                if (!response.Succeeded) return false;
                StoreTokens(response);
                return true;
            }
            catch (Exception)
            {
                Preferences.Clear();
                return false;
            }
        }

        public void StoreTokens(UserValidationResponse userValidationResponse)
        {
            IdToken = userValidationResponse.IdToken;
            RefreshToken = userValidationResponse.RefreshToken;
            Preferences.Set(AppConstants.TokenIssuer, userValidationResponse.AuthenticationProvider.ToString());
        }

        private string VerifyRedirect(string path)
        {
            var strings = path.Split('?').LastOrDefault()?.Split('&');
            if (strings == null || strings.Any(s => s.Contains("error"))) return string.Empty;
            var code = strings.SingleOrDefault(s => s.Contains("code"));
            var state = strings.SingleOrDefault(s => s.Contains("state"));
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state)) return string.Empty;

            var stateArray = state.Split('=');
            return stateArray.LastOrDefault() != m_state ? string.Empty : code.Split('=').LastOrDefault();
        }

        private async Task<bool> IsRefreshTokenValid(string refreshToken)
        {
            var requestMessage = new HttpRequestBuilder()
                .WithMethod(HttpMethod.Post)
                .WithJsonContent(new TokenResponse {RefreshToken = refreshToken})
                .WithRequestUri($"{AppConstants.RefreshEndpoint}{AuthenticationProvider}")
                .Build();

            TokenResponse tokenResponse;

            try
            {
                tokenResponse = await m_restClient.Send<TokenResponse>(requestMessage);
            }
            catch
            {
                Preferences.Clear();
                return false;
            }

            if (string.IsNullOrEmpty(tokenResponse.IdToken))
            {
                Preferences.Clear();
                return false;
            }

            IdToken = tokenResponse.IdToken;
            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken)) RefreshToken = tokenResponse.RefreshToken;

            return true;
        }
    }
}