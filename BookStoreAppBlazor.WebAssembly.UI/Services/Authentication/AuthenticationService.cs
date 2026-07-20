using Blazored.LocalStorage;
using BookStoreAppBlazor.WebAssembly.UI.Providers;
using BookStoreAppBlazor.WebAssembly.UI.Services.Base;

namespace BookStoreAppBlazor.WebAssembly.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Client httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly ApiAuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(
            Client httpClient,
            ILocalStorageService localStorage,
            ApiAuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }


        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);

            // Store JWT token
            await localStorage.SetItemAsync("accessToken", response.Token);
            Console.WriteLine("Token saved");

            var token = await localStorage.GetItemAsync<string>("accessToken");
            Console.WriteLine(token);

            // Update authentication state
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }


        public async Task Logout()
        {
            await (authenticationStateProvider).LoggedOut();
        }
    }
}