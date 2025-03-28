﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;

namespace RegisterBoook.Api.Exceptions
{
    public class RegisterBookValidator : AbstractValidator<RegisterBookRequest>
    {
        private readonly AppDbContextApi _context;
        public RegisterBookValidator(AppDbContextApi context)
        {
            _context = context;


            RuleFor(b => b.Title).NotEmpty()
                .WithMessage("Campo Título não pode estar em branco.")
                .MustAsync(TitleUnique).WithMessage("Livro já cadastrado.");
            
            RuleFor(b => b.Genere).NotEmpty().WithMessage("Campo Gênero não pode estar em branco.");
            RuleFor(b => b.AuthorId).NotEmpty().WithMessage("Código do autor não pode estar em branco.");
        }

        public async Task<bool> TitleUnique(string title, CancellationToken token)
        {
            return !await _context.books.AnyAsync(b => b.Title == title);
        }
    }

}
