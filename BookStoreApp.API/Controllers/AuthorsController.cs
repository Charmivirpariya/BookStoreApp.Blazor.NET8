using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Repositories;
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
        private readonly IAuthorsRepository authorsRepository;

        //private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(IAuthorsRepository authorsRepository, IMapper mapper, ILogger<AuthorsController> logger)
        {
            this.authorsRepository = authorsRepository;
            //_context = context;
            _mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors/?startindex=0&pagesize=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<AuthorReadOnlyDto>>> GetAuthor([FromQuery]QueryParameter queryParameter)
        {
            try
            {
                return await authorsRepository.GetAllAsync<AuthorReadOnlyDto>(queryParameter);
         
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while retrieving authors in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }
        //GET: api/Authors/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AuthorReadOnlyDto>>> GetAuthor()
        {
            try
            {
                var authors = await authorsRepository.GetAllAsync();
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
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            try
            {
                var author = await authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(GetAuthor)}");
                    return NotFound();
                }

                //var authorDto = _mapper.Map<AuthorReadOnlyDto>(author);

                return Ok(author);
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

                await authorsRepository.AddAsync(author);
                //await _context.SaveChangesAsync();

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

                var author = await authorsRepository.GetAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(PutAuthor)}");
                    return NotFound();
                }

                _mapper.Map(authorDto, author);

                await authorsRepository.UpdateAsync(author);

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
                var author = await authorsRepository.GetAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"Author with id {id} not found in {nameof(DeleteAuthor)}");
                    return NotFound();
                }

                await authorsRepository.DeleteAsync(id);

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
            return await authorsRepository.ExistsAsync(id);
        }
    }
}