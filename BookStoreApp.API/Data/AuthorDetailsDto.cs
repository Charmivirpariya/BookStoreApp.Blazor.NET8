using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Data
{
    public class AuthorDetailsDto:AuthorReadOnlyDto

    {

        public List<BookReadOnlyDto> Books { get; set; }
    }
}
