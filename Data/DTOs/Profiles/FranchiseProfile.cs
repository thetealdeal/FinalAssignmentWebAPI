using AutoMapper;
using FinalAssignmentWebAPI.Data.DTOs.FranchiseDTOs;
using FinalAssignmentWebAPI.Models;

namespace FinalAssignmentWebAPI.Data.DTOs.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(
                readDto => readDto.Movies,
                opt => opt.MapFrom(
                    domain => domain.Movies.Select(m => m.Id).ToArray()));


            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseUpdateDTO, Franchise>();
        }
    }
}
