using AutoMapper;
using FilmWebSite.BusinessLayer.DTOs;
using FilmWebSite.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.BusinessLayer.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Film, FilmDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<Actor, ActorDto>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
