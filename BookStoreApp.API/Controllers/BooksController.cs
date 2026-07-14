using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.Models.Book;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper mapper;

    public BooksController(BookStoreDbContext context,IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBook()
    {
        var bookDtos = await _context.Books.Include(b => b.Author)
            .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(bookDtos);
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // PUT: api/Book/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    
    public async Task<IActionResult> PutBook(int? id, BookUpdateDto bookDto)
    {
        if (id != bookDto.Id)
        {
            return BadRequest();
        }
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
           
        }
        mapper.Map(bookDto, book);          
        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BookExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Book
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<BookReadOnlyDto>> PostBook(BookCreateDto bookDto)
    {
        var book = mapper.Map<Book>(bookDto);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _context.Books
        .Include(b => b.Author)
        .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
        .FirstAsync(b => b.Id == book.Id);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, result);
    }

    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBook(int? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> BookExistsAsync(int? id)
    {
        return await _context.Books.AnyAsync(e => e.Id == id);
    }
}
