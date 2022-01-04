using AutoMapper;
using Core.Contracts.Common;
using Core.Contracts.OutComing;
using Core.Entities;

namespace Infrastructure.Mappings
{
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<BaseEntity, BaseDto>()
                .ReverseMap();

            CreateMap<UserEntity, UserDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap();
        }
    }
}
