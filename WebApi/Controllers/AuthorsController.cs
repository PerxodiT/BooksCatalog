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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsScenaries _scenaries;

        public AuthorsController(ApplicationContext context) => 
            _scenaries = new AuthorsScenaries(context);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAuthorVm>>> GetAuthors() =>
            await _scenaries.GetAuthor();

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAuthorVm>> GetAuthor(int id) => 
            await _scenaries.GetAuthor(id);

        [HttpGet("{id}/Books")]
        public async Task<ActionResult<IEnumerable<GetBookVm>>> GetBooks(int id) =>
            await _scenaries.GetBooks(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAuthorVm author)
        {
            var result = await _scenaries.UpdateAuthor(id, author);
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
        public async Task<IActionResult> PostAuthor([FromBody] AddAuthorVm author)
        {
            var result = await _scenaries.AddAuthor(author);
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
            var result = await _scenaries.DeleteAuthor(id);
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
