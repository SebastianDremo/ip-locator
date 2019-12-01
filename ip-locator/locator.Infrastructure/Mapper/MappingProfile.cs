using AutoMapper;
using locator.Core.Entities;
using locator.Core.Models;

namespace locator.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Localization, LocalizationModel>();
            CreateMap<LocalizationModel, Localization>();
        }
    }
}