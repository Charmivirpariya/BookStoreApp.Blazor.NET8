using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthor()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                var authorDtos = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors);

                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while retrieving authors in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(GetAuthor)}");
                    return NotFound();
                }

                var authorDto = _mapper.Map<AuthorReadOnlyDto>(author);

                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while retrieving author with id {id} in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Authors
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorReadOnlyDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                var readOnlyDto = _mapper.Map<AuthorReadOnlyDto>(author);

                logger.LogInformation($"Author with id {author.Id} was created successfully.");

                return CreatedAtAction(
                    nameof(GetAuthor),
                    new { id = author.Id },
                    readOnlyDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while creating an author in {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            try
            {
                if (id != authorDto.Id)
                {
                    logger.LogWarning($"Invalid update request. Route id {id} does not match DTO id {authorDto.Id}.");
                    return BadRequest();
                }

                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(PutAuthor)}");
                    return NotFound();
                }

                _mapper.Map(authorDto, author);

                await _context.SaveChangesAsync();

                logger.LogInformation($"Author with id {id} updated successfully.");

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AuthorExists(id))
                {
                    logger.LogWarning($"Author with id {id} no longer exists.");
                    return NotFound();
                }

                logger.LogError(ex, $"Concurrency error occurred while updating author with id {id}.");
                return StatusCode(500, Messages.Error500Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating author with id {id} in {nameof(PutAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(DeleteAuthor)}");
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                logger.LogInformation($"Author with id {id} deleted successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting author with id {id} in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
    }
}