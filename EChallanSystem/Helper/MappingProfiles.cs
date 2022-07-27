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
            CreateMap<CitizenDTO, Citizen>();
            CreateMap<Challan, ChallanDTO>();
            CreateMap<ChallanDTO, Challan>();
            CreateMap<TrafficWardenDTO, TrafficWarden>();
            CreateMap<TrafficWarden, TrafficWardenDTO>();
            CreateMap<VehicleDTO,Vehicle>();
            CreateMap<Vehicle,VehicleDTO>();
        
         
        }
    }
}
