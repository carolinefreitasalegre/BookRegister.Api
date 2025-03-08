using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Dto.Responses
{
    public class RegisterBookResponse
    {
        public string Title {  get; set; } = string.Empty;
        public string Genere { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
}
