using AutoMapper;
using System.Security.Claims;
using konatus.api.ViewModels;
using konatus.business.Models;

namespace konatus.api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<MaintenanceModel, MaintenanceViewModel>()
            .ReverseMap();

            CreateMap<StageModel, StageViewModel>()
            .ReverseMap();

            CreateMap<Claim, ClaimViewModel>().ReverseMap();
        }
    }
}