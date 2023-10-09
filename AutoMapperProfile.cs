using AutoMapper;
using home_libraryAPI.DTOs;
using home_libraryAPI.Models;

namespace home_libraryAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
        }


    }
}
