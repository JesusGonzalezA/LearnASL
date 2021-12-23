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

            CreateMap<ITestDto, ITest>()
                .ReverseMap();
            CreateMap<IQuestion, IQuestionDto>()
                .ReverseMap();

            CreateMap<TestOptionWordToVideoEntity, TestOptionWordToVideoDto>()
                .IncludeBase<ITest, ITestDto>();
            CreateMap<QuestionOptionWordToVideoEntity, QuestionOptionWordToVideoDto>()
                .IncludeBase<IQuestion, IQuestionDto>();

            CreateMap<TestOptionVideoToWordEntity, TestOptionVideoToWordDto>()
                .IncludeBase<ITest, ITestDto>();
            CreateMap<QuestionOptionVideoToWordEntity, QuestionOptionVideoToWordDto>()
                .IncludeBase<IQuestion, IQuestionDto>();
        }
    }
}
