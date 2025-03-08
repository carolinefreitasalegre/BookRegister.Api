using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Exceptions;
using RegisterBoook.Api.Service.Interfaces;
using RegisterBoook.Api.Services.Interfaces;

namespace RegisterBoook.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRegisterBook _register;
        private readonly IRegisterAuthor _registerAuthor;
        private readonly IValidator<RegisterBookRequest> _registerBookValidator;
        private readonly IValidator<RegisterAuthorRequest> _registerAuthorValidator;



        public BookController(IRegisterBook register, IValidator<RegisterBookRequest> registerBookValidator, IRegisterAuthor registerAuthor, IValidator<RegisterAuthorRequest> registerAuthorValidator)
        {
            _register = register;
            _registerBookValidator = registerBookValidator;
            _registerAuthor = registerAuthor;
            _registerAuthorValidator = registerAuthorValidator;
        }

        [HttpGet("listar-livros")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _register.GetAllBooks();
            return Created("", result);
        }

        [HttpPost("/cadastrar-livro")]
        public async Task<IActionResult> CreateBook(RegisterBookRequest request)
        {

            var validatorResult = await _registerBookValidator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var register = await _register.CreateBook(request);

            return Created("", register);

        }

        [HttpPost("/editar-livro/{Id}")]
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

        [HttpDelete("/deletar-livro/{Id}")]
        public async Task<IActionResult> DeleteBook(Guid Id)
        {
            var result = await _register.DeleteBook(Id);
            return Ok(result);
        }



        [HttpGet("/listar-autores")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var listAuthors = await _registerAuthor.GetAllAuthors();

            return Created("", listAuthors);
        }



        [HttpPost("/cadastrar-autor")]
        public async Task<IActionResult> CreateAuthor(RegisterAuthorRequest request)
        {
            var validatorResult = await _registerAuthorValidator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var newAutor = await _registerAuthor.CreateAuthor(request);

            return Created("", newAutor);
        }


        [HttpPost("/editar-autor/{Id}")]
        public async Task<IActionResult> EditAuthor(Guid Id, EditAuthorRequest request)
        {

            request.Id = Id;

            if (request == null)
            {
                return NotFound();
            }

            var editAuthor = await _registerAuthor.EditAuthor(request);

            return Created("", editAuthor);
        }


        [HttpDelete("/deletar-autor/{Id}")]
        public async Task<IActionResult> DeleteAuthor(Guid Id)
        {
            var result = await _registerAuthor.DeleteAuthor(Id);

            return Ok(result);

        }

    }
}
