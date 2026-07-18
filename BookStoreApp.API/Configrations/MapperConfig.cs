using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.User;

namespace BookStoreApp.API.Configrations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorDetailsDto>();
            CreateMap<Author, AuthorReadOnlyDto>();

            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(map =>$"{map.Author.FirstName} {map.Author.LastName}"));
            CreateMap<Book, BookDetailsDto>()
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(map =>$"{map.Author.FirstName} {map.Author.LastName}"));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}