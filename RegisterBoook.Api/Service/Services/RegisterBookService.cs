using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto;
using RegisterBoook.Api.Models;
using RegisterBoook.Api.Services.Interfaces;

namespace RegisterBoook.Api.Service.Services
{
    public class RegisterBookService : IRegisterBook
    {

        private readonly AppDbContextApi _context;

        public RegisterBookService(AppDbContextApi context)
        {
            _context = context;
        }


        public async Task<List<Book>> GetAllBooks()
        {

            try
            {
                var listBooks = await _context.books.ToListAsync();
                return await _context.books.ToListAsync();

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

                var newBook = new Book(request.Title, request.Genere)
                {
                    Id = Guid.NewGuid(),
                    Register = DateTime.Now,
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
                var book = await _context.books.FirstOrDefaultAsync();

                if (book == null)
                {
                    throw new KeyNotFoundException("Livro não encontrado.");
                }

                book.Title = request.Title;
                book.Genere = request.Genere;

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

                var book = await _context.books.FirstOrDefaultAsync(b => b.Id == Id);

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

