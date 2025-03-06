using System.ComponentModel.DataAnnotations;

namespace RegisterBoook.Api.Dto.Requests
{
    public class EditBookRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;
        public string Genere { get; set; } = string.Empty;
    }
}
