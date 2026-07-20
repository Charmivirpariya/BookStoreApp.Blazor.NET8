using BookStoreAppBlazor.WebAssembly.UI.Services.Base;

namespace BookStoreAppBlazor.WebAssembly.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);

        public Task Logout();
    }
}