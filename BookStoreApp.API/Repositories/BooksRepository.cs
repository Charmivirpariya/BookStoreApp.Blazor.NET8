using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public BooksRepository(BookStoreDbContext context,IMapper mapper) : base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<BookReadOnlyDto> CreateBook(Book book)
        {
            await AddAsync(book);

            return await context.Books
           .Include(b => b.Author)
           .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
           .FirstAsync(b => b.Id == book.Id);
        }

        public async Task<List<BookReadOnlyDto>> GetAllBooksAsync()
        {
            return await context.Books.Include(b => b.Author)
            .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<BookDetailsDto> GetBookAsync(int id)
        {
            return await context.Books.Include(b => b.Author)
            .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
