using System;
using System.Security.Cryptography;
using System.Text;

namespace BrewView.Services.Account
{
    public static class PKCE
    {
        private const int recommendedNumberOfBits = 64;

        public static string GenerateCodeChallenge(string codeVerifier)
        {
            var hasher = SHA256.Create();
            var byteArray = hasher.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier));
            var base64UrlString = Convert.ToBase64String(byteArray, Base64FormattingOptions.None).Replace('+', '-').Replace('/', '_').TrimEnd('=');
            return base64UrlString;
        }

        public static string GenerateSecureString()
        {
            var base64UrlString = Convert.ToBase64String(RandomStringGenerator(), Base64FormattingOptions.None).Replace('+', '-').Replace('/', '_').TrimEnd('=');
            return base64UrlString;
        }

        private static byte[] RandomStringGenerator()
        {
            var randomBytes = new byte[recommendedNumberOfBits];
            var rNG = new RNGCryptoServiceProvider();
            rNG.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}