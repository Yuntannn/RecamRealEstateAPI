using AutoMapper;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CurrentUserDto>();

            CreateMap<ListingCase, ListingCaseRequestDto>();
            CreateMap<ListingCaseRequestDto, ListingCase>();

            CreateMap<ListingCase, ListingCaseDto>();
            CreateMap<ListingCaseDto, ListingCase>();

            CreateMap<MediaAsset, MediaDto>();

            CreateMap<MediaRequestDto, MediaAsset>();
        }
    }
}
