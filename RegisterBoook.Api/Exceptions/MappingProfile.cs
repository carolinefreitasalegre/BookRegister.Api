using AutoMapper;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Exceptions
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, RegisterBookResponse>();
        }
    }
}
