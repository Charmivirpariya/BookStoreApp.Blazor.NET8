using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;

namespace BookStoreApp.API.Configrations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorReadOnlyDto>();
        }
    }
}