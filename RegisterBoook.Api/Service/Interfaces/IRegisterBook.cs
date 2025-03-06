using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Services.Interfaces
{
    public interface IRegisterBook
    {
        Task<List<RegisterBookResponse>> GetAllBooks();
        Task<Book> CreateBook(RegisterBookRequest request);

        Task<Book> EditBook(EditBookRequest request);

        Task<Book> DeleteBook(Guid Id);
    }
}
