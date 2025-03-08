using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Service.Interfaces
{
    public interface IRegisterAuthor
    {

        Task<List<RegisterAuthorResponse>> GetAllAuthors();
        Task<Author> CreateAuthor(RegisterAuthorRequest request);

        Task<Author> EditAuthor(EditAuthorRequest request);

        Task<Author> DeleteAuthor(Guid Id);

    }
}
