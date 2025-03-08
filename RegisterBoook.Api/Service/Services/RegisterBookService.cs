using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;
using RegisterBoook.Api.Services.Interfaces;

namespace RegisterBoook.Api.Service.Services
{
    public class RegisterBookService : IRegisterBook
    {

        private readonly AppDbContextApi _context;
        private readonly IMapper _mapping;
        public RegisterBookService(AppDbContextApi context, IMapper mapping)
        {
            _context = context;
            _mapping = mapping;
        }


        public async Task<List<RegisterBookResponse>> GetAllBooks()
        {

            try
            {
                var listBooks = await _context.books.Include(a => a.Author).ToListAsync();

                var responseList = _mapping.Map<List<RegisterBookResponse>>(listBooks);

                return responseList;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao buscar livros.", ex);
            }

        }


        public async Task<Book> CreateBook(RegisterBookRequest request)
        {
            try
            {
                var authorExist = await _context.author.FirstOrDefaultAsync(x => x.Id == request.AuthorId) ?? throw new KeyNotFoundException("Autor não encontrado.");
                var newBook = new Book(request.Title, request.Genere)
                {

                    Id = Guid.NewGuid(),
                    Register = DateTime.Now,
                    Author = authorExist,

                };


                _context.books.Add(newBook);
                await _context.SaveChangesAsync();


                return newBook;

            }
            catch (DbUpdateException ex)
            {

                throw new Exception("Erro ao salvar no banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao cadastrar o livro.", ex);
            }
        }

        public async Task<Book> EditBook(EditBookRequest request)
        {

            try
            {
                var book = await _context.books.Include(a => a.Author).FirstOrDefaultAsync(b => b.Id == request.Id);
                var author = await _context.author.FirstOrDefaultAsync(a => a.Id == request.Author.Id);

                if (book == null)
                {
                    throw new KeyNotFoundException("Livro não encontrado.");
                }
                if (author == null)
                {
                    throw new KeyNotFoundException("Autor não encontrado.");
                }

                book.Title = request.Title;
                book.Genere = request.Genere;
                book.Author = author;


                _context.books.Update(book);
                await _context.SaveChangesAsync();

                return book;
            }
            catch (DbUpdateException ex)
            {

                throw new Exception("Erro ao salvar no banco de dados", ex);
            }

        }

        public async Task<Book> DeleteBook(Guid Id)
        {
            try
            {

                var book = await _context.books.FirstOrDefaultAsync(b => b.Id == Id) ?? throw new KeyNotFoundException("Livro não encontrado");
                _context.books.Remove(book);
                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao salvar no banco de dados", ex);
            }
        }
    }
}

