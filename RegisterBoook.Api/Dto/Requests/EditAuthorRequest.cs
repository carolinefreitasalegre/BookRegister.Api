namespace RegisterBoook.Api.Dto.Requests
{
    public class EditAuthorRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
