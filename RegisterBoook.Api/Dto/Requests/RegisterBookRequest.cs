using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Dto.Requests
{
    public class RegisterBookRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Genere { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
