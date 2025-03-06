using RegisterBoook.Api.Dto;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Services.Interfaces
{
    public interface IRegisterBook
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> CreateBook(RegisterBookRequest request);

        Task<Book> EditBook(EditBookRequest request);

        Task<Book> DeleteBook(Guid Id);
    }
}
