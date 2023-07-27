using AutoMapper;
using FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs;
using FinalAssignmentWebAPI.Models;

namespace FinalAssignmentWebAPI.Data.DTOs.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() { 
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(
                readDto => readDto.Characters,
                opt => opt.MapFrom(
                    domain => domain.Characters.Select(c => c.Id).ToArray()));  // this converts the characters list to a int Id list

            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();

        }
    }
}
