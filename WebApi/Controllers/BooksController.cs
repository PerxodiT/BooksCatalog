using DB;
using DB.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB.Scenaries;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksScenaries _scenaries;

        public BooksController(ApplicationContext context) => 
            _scenaries = new BooksScenaries(context);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookVm>>> GetBooks() =>
            await _scenaries.GetBook();

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookVm>> GetBook(int id) =>
            await _scenaries.GetBook(id);

        [HttpGet("{id}/Authors")]
        public async Task<ActionResult<IEnumerable<GetAuthorVm>>> GetAuthors(int id) =>
            await _scenaries.GetAuthors(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, UpdateBookVm book)
        {
            var result = await _scenaries.UpdateBook(id, book);
            switch (result)
            {
                case (int)Errors.BadRequest:
                    return BadRequest();
                case (int)Errors.NotFound:
                    return NotFound();
                default:
                    return Ok();
            }
        }

        [HttpPut("{id}/AddAuthor")]
        public async Task<IActionResult> PutBook(int id, int AuthorId)
        {
            var result = await _scenaries.AddAuthor(id, AuthorId);
            switch (result)
            {
                case (int)Errors.BadRequest:
                    return BadRequest();
                case (int)Errors.NotFound:
                    return NotFound();
                default:
                    return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] AddBookVm book)
        {
            var result = await _scenaries.AddBook(book);
            switch (result)
            {
                case (int)Errors.BadRequest:
                    return BadRequest();
                case (int)Errors.NotFound:
                    return NotFound();
                default:
                    return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _scenaries.DeleteBook(id);
            switch (result)
            {
                case (int)Errors.BadRequest:
                    return BadRequest();
                case (int)Errors.NotFound:
                    return NotFound();
                default:
                    return Ok();
            }
        }
    }
}
