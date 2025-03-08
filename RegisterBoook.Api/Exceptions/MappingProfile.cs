using AutoMapper;
using RegisterBoook.Api.Dto.Responses;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.Exceptions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, RegisterAuthorResponse>().ForMember(b => b.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Book, RegisterBookResponse>().ForMember(b => b.AuthorName, opt => opt.MapFrom(src => src.Author.Name)).ReverseMap();

        }
    }
}
