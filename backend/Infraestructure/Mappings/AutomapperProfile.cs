using AutoMapper;
using Core.Contracts.Common;
using Core.Contracts.Incoming;
using Core.Contracts.OutComing;
using Core.Entities;
using Core.Entities.Tests;

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

            CreateMap<TestOptionWordToVideoEntity, TestOptionWordToVideoDto>()
                .IncludeBase<BaseEntity, BaseDto>();
            CreateMap<QuestionOptionWordToVideoEntity, QuestionOptionWordToVideoDto>()
                .IncludeBase<BaseEntity, BaseDto>();
        }
    }
}
