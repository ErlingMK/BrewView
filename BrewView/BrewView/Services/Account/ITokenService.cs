using System.Threading.Tasks;
using BrewView.Models;

namespace BrewView.Services.Account
{
    public interface ITokenService
    {
        Task<bool> HasValidTokens();
        (string IdToken, string RefreshToken) GetTokens();
        Task ProviderSignIn(string provider);
        Task<bool> ProviderTokenRequest(string path);
        void StoreTokens(UserValidationResponse userValidationResponse);
        string IdToken { get; }
        string RefreshToken { get; }
        string AuthenticationProvider { get; }
    }
}