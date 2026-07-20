using Blazored.LocalStorage;
namespace BookStoreAppBlazor.WebAssembly.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly Client client;
        private readonly ILocalStorageService localStorage;

        public BaseHttpService(Client client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }
        protected Response<Guid> ConvertApiException<Guid>(ApiException e)
        {
            if(e.StatusCode == 400)
            {
                return new Response<Guid>
                {
                    Message = "Validation errors have occurred.",
                    ValidationErrors = e.Response,
                    Success = false
                };
            }
            if (e.StatusCode == 404)
            {
                return new Response<Guid>
                {
                    Message = "The requested resource was not found.",
                    ValidationErrors = e.Response,
                    Success = false
                };
            }
            if(e.StatusCode >=200 && e.StatusCode <= 299) { 
                return new Response<Guid>
                {
                    Message = "Operation successful.",
                    ValidationErrors = e.Response,
                    Success = true
                };
            }
            else
            {
                return new Response<Guid>
                {
                    Message = "Something went wrong, please try again.",
                    Success = false
                };
            }
        }
        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
