using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();
    }
}