using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> GetBooks();

        Task<Response<BookDetailsDto>> GetBookById(int id);

        Task<Response<BookUpdateDto>> GetBookForUpdate(int id);

        Task<Response<int>> CreateBook(BookCreateDto book);

        Task<Response<int>> UpdateBook(int id, BookUpdateDto book);

        Task<Response<int>> Delete(int id);
    }
}
