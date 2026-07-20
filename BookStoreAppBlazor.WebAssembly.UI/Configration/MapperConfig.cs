using AutoMapper;
using BookStoreAppBlazor.WebAssembly.UI.Services.Base;

namespace BookStoreAppBlazor.WebAssembly.UI.Configration
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
