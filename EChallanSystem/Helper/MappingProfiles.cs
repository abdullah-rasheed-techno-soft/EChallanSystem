using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;

namespace EChallanSystem.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Citizen, CitizenDTO>();
            CreateMap<Challan, ChallanDTO>();
            CreateMap<ChallanDTO, Challan>();
        }
    }
}
