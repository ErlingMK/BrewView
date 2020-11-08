using System.Threading.Tasks;
using BrewView.Contracts.User;
using BrewView.Models;

namespace BrewView.Services.Account
{
    public interface IAccountService
    {
        Task<UserValidationResponse> SignIn(CredentialsModel credentials);
        Task SignIn(string provider);
        Task<UserValidationResponse> RegisterUser(CredentialsModel credentials);
        Task<bool> ProviderTokenRequest(string intentDataString);
    }
}