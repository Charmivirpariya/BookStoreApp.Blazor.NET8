using AutoMapper;
using Blazored.LocalStorage;
using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services
{
        public class BookService : BaseHttpService, IBookService
        {
            private readonly Client client;
            private readonly IMapper mapper;

            //private readonly ILocalStorageService localStorage;

            public BookService(Client client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
            {
                this.client = client;
                this.mapper = mapper;
                //this.localStorage = localStorage;
            }

            public async Task<Response<int>> CreateBook(BookCreateDto book)
            {
                Response<int> response = new();

                try
                {
                    await GetBearerToken();

                    await client.BooksPOSTAsync(book);


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

                    await client.BooksDELETEAsync(id);


                }
                catch (ApiException exception)
                {
                    response = ConvertApiException<int>(exception);
                }

                return response;
            }

            public async Task<Response<BookDetailsDto>> GetBookById(int id)
            {
                Response<BookDetailsDto> response;
                try
                {
                    await GetBearerToken();
                    var data = await client.BooksGETAsync(id);
                    response = new Response<BookDetailsDto>
                    {
                        Data = data,
                        Success = true
                    };
                }
                catch (ApiException exception)
                {
                    response = ConvertApiException<BookDetailsDto>(exception);
                }
                return response;
            }

            public async Task<Response<BookUpdateDto>> GetBookForUpdate(int id)
            {
                Response<BookUpdateDto> response;

                try
                {
                    await GetBearerToken();

                    var data = await client.BooksGETAsync(id);

                    response = new Response<BookUpdateDto>
                    {
                        Success = true,
                        Data = new BookUpdateDto
                        {
                            Id = data.Id,
                            Title = data.Title,
                            Year = data.Year,
                            Isbn = data.Isbn,
                            Summary = data.Summary,
                            Image = data.Image,
                            Price = data.Price,
                            AuthorId = data.AuthorId
                        }
                    };
                }
                catch (ApiException exception)
                {
                    response = ConvertApiException<BookUpdateDto>(exception);
                }

                return response;
            }

            public async Task<Response<List<BookReadOnlyDto>>> GetBooks()
            {
                Response<List<BookReadOnlyDto>> response;
                try
                {
                    await GetBearerToken();
                    var data = await client.BooksAllAsync();
                    response = new Response<List<BookReadOnlyDto>>
                    {
                        Data = data.ToList(),
                        Success = true
                    };
                }
                catch (ApiException exception)
                {
                    response = ConvertApiException<List<BookReadOnlyDto>>(exception);
                }
                return response;
            }

            public async Task<Response<int>> UpdateBook(int id, BookUpdateDto book)
            {
                Response<int> response = new();

                try
                {
                    await GetBearerToken();

                    await client.BooksPUTAsync(id, book);

                    response.Success = true;
                    response.Message = "Book updated successfully";
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
