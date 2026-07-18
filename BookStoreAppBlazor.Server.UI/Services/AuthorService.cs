using AutoMapper;
using Blazored.LocalStorage;
using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly Client client;
        private readonly IMapper mapper;

        //private readonly ILocalStorageService localStorage;

        public AuthorService(Client client, ILocalStorageService localStorage,IMapper mapper) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
            //this.localStorage = localStorage;
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();

                await client.AuthorsPOSTAsync(author);


            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> Delete(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();

                await client.AuthorsDELETEAsync(id);


            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

        public async Task<Response<AuthorDetailsDto>> GetAuthorById(int id)
        {
            Response<AuthorDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthorDetailsDto>(exception);
            }
            return response;
        }

        public async Task<Response<AuthorUpdateDto>> GetAuthorForUpdate(int id)
        {
            Response<AuthorUpdateDto> response;

            try
            {
                await GetBearerToken();

                var data = await client.AuthorsGETAsync(id);

                response = new Response<AuthorUpdateDto>
                {
                    Success = true,
                    Data = new AuthorUpdateDto
                    {
                        Id = data.Id,         
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Bio = data.Bio
                    }
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthorUpdateDto>(exception);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
        {
            Response<List<AuthorReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<AuthorReadOnlyDto>>(exception);
            }
            return response;
        }

        public async Task<Response<int>> UpdateAuthor(int id, AuthorUpdateDto author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();

                await client.AuthorsPUTAsync(id, author);

                response.Success = true;
                response.Message = "Author updated successfully";
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
                Console.WriteLine(exception.Response);
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

    }
}
