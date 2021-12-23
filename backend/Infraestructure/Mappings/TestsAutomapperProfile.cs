using AutoMapper;
using Core.Contracts.OutComing.Tests;
using Core.Entities.Tests;

namespace Infraestructure.Mappings
{
    public class TestsAutomapperProfile : Profile
    {
        public TestsAutomapperProfile()
        {
            CreateMap<ISimpleTestDto, ITest>()
                .ReverseMap();
            CreateMap<ITestDto, ITest>()
                .ReverseMap();
            CreateMap<IQuestion, IQuestionDto>()
                .ReverseMap();

            CreateMap<TestOptionWordToVideoEntity, TestOptionWordToVideoDto>()
                .IncludeBase<ITest, ITestDto>();
            CreateMap<QuestionOptionWordToVideoEntity, QuestionOptionWordToVideoDto>()
                .IncludeBase<IQuestion, IQuestionDto>()
                .ForMember(dto => dto.CorrectAnswer, value => value.MapFrom(entity => (entity.UserAnswer == null)? null : entity.CorrectAnswer ));

            CreateMap<TestOptionVideoToWordEntity, TestOptionVideoToWordDto>()
                .IncludeBase<ITest, ITestDto>();
            CreateMap<QuestionOptionVideoToWordEntity, QuestionOptionVideoToWordDto>()
                .IncludeBase<IQuestion, IQuestionDto>()
                .ForMember(dto => dto.CorrectAnswer, value => value.MapFrom(entity => (entity.UserAnswer == null) ? null : entity.CorrectAnswer));
        }
    }
}
