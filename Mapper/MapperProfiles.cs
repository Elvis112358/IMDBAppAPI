using DTO;
using EF = EFModels;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using Helpers;

namespace Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<EF.Actor, Actor>().ReverseMap();
            CreateMap<EF.ActorMovie, ActorMovie>().ReverseMap();
            CreateMap<EF.Movie, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorMovies))
            .ReverseMap(); 
            CreateMap<PagedList<EF.Movie>, PagedList<Movie>>().ReverseMap();
            CreateMap<EF.User, User>().ReverseMap();

            // CreateMap<EF.Publisher, Publisher>().ReverseMap();
            CreateMap<EF.SearchFeature, SearchFeature>().ReverseMap();
        }
    }
}