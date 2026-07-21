using Blazored.LocalStorage;
using BookStoreAppBlazor.Server.UI.Providers;
using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly Client httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly ApiAuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(
            Client httpClient,
            ILocalStorageService localStorage,
            ApiAuthenticationStateProvider authenticationStateProvider):base(httpClient,localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel)
        {
            Response<AuthResponse> response;
            var result = await httpClient.LoginAsync(loginModel);
            response = new Response<AuthResponse>
            {
                Data = result,
                Success = true,
            };

            // Store JWT token
            await localStorage.SetItemAsync("accessToken", result.Token);
            Console.WriteLine("Token saved");

            var token = await localStorage.GetItemAsync<string>("accessToken");
            Console.WriteLine(token);

            // Update authentication state
            await((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return response;
        }

        public async Task Logout()
        {
            await (authenticationStateProvider).LoggedOut();
        }

        
    }
}