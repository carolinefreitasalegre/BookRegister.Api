using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;
using RegisterBoook.Api.Service.Interfaces;

namespace RegisterBoook.Api.Service.Services
{
    public class RegisterAuthorService : IRegisterAuthor
    {
		private readonly AppDbContextApi _context;
        private readonly IMapper _mapping;

        public RegisterAuthorService(AppDbContextApi context, IMapper mapping)
        {
            _context = context;
            _mapping = mapping;
        }


        public async Task<List<RegisterAuthorResponse>> GetAllAuthors()
        {
            try
            {
                var listAuhors = await _context.author.ToListAsync();

                var responseList = _mapping.Map<List<RegisterAuthorResponse>>(listAuhors);

                return responseList;



            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao buscar autores.", ex);
            }
        }




        public async Task<Author> CreateAuthor(RegisterAuthorRequest request)
        {
			try
			{
				var newAuthor = new Author()
				{
					Name = request.Name,
				};

				_context.author.Add(newAuthor);
				await _context.SaveChangesAsync();
				return newAuthor;

			}
            catch (DbUpdateException ex)
            {

                throw new Exception("Erro ao salvar no banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao cadastrar o autor.", ex);
            }
        }

       

       
        public async Task<Author> EditAuthor(EditAuthorRequest request)
        {
            try
            {
                var author = _context.author.FirstOrDefault(a => a.Id == request.Id) ?? throw new Exception("Autor não encontrado.");

                //existAuthor.Id = request.Id;
                author.Name = request.Name;

                _context.author.Update(author);
                await _context.SaveChangesAsync();

                return author;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao salvar dados", ex);
            }
        }


        public async Task<Author> DeleteAuthor(Guid Id)
        {
            try
            {

                var author = _context.author.FirstOrDefault(a => a.Id == Id) ?? throw new Exception("Autor não encontrado");

                _context.author.Remove(author);
                await _context.SaveChangesAsync();

                return author;

                
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar deletar Autor", ex);
            }
        }
    }
}
