using AutoMapper;
using BookStoreAppBlazor.Server.UI.Services.Base;

namespace BookStoreAppBlazor.Server.UI.Configration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<AuthorCreateDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, AuthorUpdateDto>().ReverseMap();
        }
    }
}
