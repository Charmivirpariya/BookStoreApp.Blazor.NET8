using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthors();

        Task<Response<AuthorDetailsDto>> GetAuthorById(int id);

        Task<Response<AuthorUpdateDto>> GetAuthorForUpdate(int id);

        Task<Response<int>> CreateAuthor(AuthorCreateDto author);

        Task<Response<int>> UpdateAuthor(int id, AuthorUpdateDto author);

        Task<Response<int>> Delete(int id);
    }
}
