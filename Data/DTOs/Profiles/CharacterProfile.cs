using AutoMapper;
using FinalAssignmentWebAPI.Data.DTOs.CharacterDTOs;
using FinalAssignmentWebAPI.Data.DTOs.MoviesDTOs;
using FinalAssignmentWebAPI.Models;

namespace FinalAssignmentWebAPI.Data.DTOs.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(
                readDto => readDto.Movies,
                opt => opt.MapFrom(
                    domain => domain.Movies.Select(m => m.Id).ToArray()));


            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterUpdateDTO, Character>();
        }
    }
}
