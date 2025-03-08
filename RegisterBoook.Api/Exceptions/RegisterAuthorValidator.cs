using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;

namespace RegisterBoook.Api.Exceptions
{
    public class RegisterAuthorValidator : AbstractValidator<RegisterAuthorRequest>
    {

        private readonly AppDbContextApi _context;

        public RegisterAuthorValidator(AppDbContextApi context)
        {
            _context = context;

            RuleFor(a => a.Name).NotEmpty().WithMessage("Campo nome não pode estar em branco.")
                .MustAsync(NameUnique).WithMessage("Livro já cadastrado."); ;
        }

        public async Task<bool> NameUnique(string name, CancellationToken token)
        {
            return !await _context.author.AnyAsync(a => a.Name == name);
        }
    }
}
