namespace BrewView
{
    public static class AppConstants
    {
        public const string BaseAddress = "https://192.168.1.150:5566"; // TODO: Make this settable

        // Preferences keys
        public const string TokenIssuer = "TokenIssuer";
        public const string RefreshToken = "RefreshToken";
        public const string IdToken = "IdToken";

        // Endpoints
        public const string SignInEndPoint = "api/auth/user/signin";
        public const string AccountRegistrationEndpoint = "api/auth/user/create";
        public const string RefreshEndpoint = "api/auth/user/refresh/";
        public const string AuthenticationRequestEndpoint = "api/auth/user/";
        public const string TokenRequestEndpoint = "api/auth/user/redirect/";
    }
}