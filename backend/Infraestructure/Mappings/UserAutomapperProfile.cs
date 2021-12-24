using AutoMapper;
using Core.Contracts.Common;
using Core.Contracts.OutComing;
using Core.Entities;

namespace Infraestructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<BaseEntity, BaseDto>()
                .ReverseMap();

            CreateMap<UserEntity, UserDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
        }
    }
}
