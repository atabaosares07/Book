using AutoMapper;
using Book.Data.Entities;
using Book.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // entity -> dto
            CreateMap<Category, CategoryDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Book.Data.Entities.Book, BookDto>();
            CreateMap<BookAuthor, BookAuthorDto>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(o => o.UserId))
                .ForMember(dest => dest.Roles, src => src.MapFrom(o => o.UserInRoles.Select(s => s.Role)));

            // entity -> query dto
            CreateMap<Category, CategoryQueryDto>();
            CreateMap<Author, AuthorQueryDto>();
            CreateMap<Book.Data.Entities.Book, BookQueryDto>()
                .ForMember(dest => dest.Details, o => o.MapFrom(src => string.Format("bookName: {0}, isbn: {1}", src.BookName, src.Isbn)))
                .ForMember(dest => dest.Message, o => o.MapFrom(src => string.Format("Hello world! Book ID is {0}", src.BookId)))
                ;
            CreateMap<Publisher, PublisherQueryDto>();

            // dto -> entity
            CreateMap<CategoryDto, Category>();
            CreateMap<AuthorDto, Author>();
            CreateMap<BookDto, Book.Data.Entities.Book>();
            CreateMap<BookAuthorDto, BookAuthor>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<RoleDto, Role>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(o => o.Id));
            CreateMap<RegisterDto, User>();
        }
    }
}
