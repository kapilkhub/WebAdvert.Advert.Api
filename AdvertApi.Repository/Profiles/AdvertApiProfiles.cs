using AdvertApi.Models;
using AdvertApi.Repository.DTO;
using AutoMapper;

namespace AdvertApi.Repository.Profiles
{
    public class AdvertApiProfiles : Profile
    {
        public AdvertApiProfiles()
        {
            CreateMap<AdvertDTO, AdvertModel>().ReverseMap();
        }
    }
}
