namespace BrewView.Models
{
    public class UserValidationResponse
    {
        public bool Succeeded { get; set; }
        public UserValidationResponseMessage Message { get; set; }
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public AuthenticationProvider AuthenticationProvider { get; set; }
    }

    public enum UserValidationResponseMessage
    {
        UserExists = 0,
        UserCreated = 1,
        InvalidPassword = 2,
        UserDoesNotExist = 3,
        SignedIn = 4
    }

    public enum AuthenticationProvider
    {
        Google,
    }
}