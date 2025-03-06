using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RegisterBoook.Api.Dto;
using RegisterBoook.Api.Services.Interfaces;

namespace RegisterBoook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRegisterBook _register;
        private readonly IValidator<RegisterBookRequest> _RegisterBookValidator;


        public BookController(IRegisterBook register, IValidator<RegisterBookRequest> registerBookValidator)
        {
            _register = register;
            _RegisterBookValidator = registerBookValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _register.GetAllBooks());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(RegisterBookRequest request)
        {

            var validatorResult = await _RegisterBookValidator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var register = await _register.CreateBook(request);

            return Created("", register);

        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> EditBook(Guid Id, EditBookRequest request)
        {

            request.Id = Id;

            if (request == null)
            {
                return NotFound();
            }

            var editBook = await _register.EditBook(request);
            return Created("", editBook);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBook(Guid Id)
        {
            var result = await _register.DeleteBook(Id);
            return Ok(result);
        }
    }
}
