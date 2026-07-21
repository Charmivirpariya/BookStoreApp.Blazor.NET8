using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.Models.Book;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using BookStoreApp.API.Repositories;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository booksRepository;
    private readonly IMapper mapper;

    public BooksController(IBooksRepository booksRepository,IMapper mapper)
    {
        this.booksRepository= booksRepository;
        this.mapper = mapper;
    }

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBook()
    {
        var bookDtos = await booksRepository.GetAllBooksAsync();
        return Ok(bookDtos);
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
    {
        var book = await booksRepository.GetBookAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // PUT: api/Book/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    
    public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
    {
        if (id != bookDto.Id)
        {
            return BadRequest();
        }
        var book = await booksRepository.GetAsync(id);
        if (book == null)
        {
            return NotFound();
           
        }
        mapper.Map(bookDto, book);          
       

        try
        {
            await booksRepository.UpdateAsync(book);
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

        var result = await booksRepository.CreateBook(book);

        return CreatedAtAction(nameof(GetBook), new { id = result.Id }, result);
    }

    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await booksRepository.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }
   
        await booksRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> BookExistsAsync(int id)
    {
        return await booksRepository.ExistsAsync(id);
    }
}
