using AutoMapper;
using Core.Contracts.Common;
using Core.Contracts.Incoming;
using Core.Contracts.OutComing.Tests;
using Core.Entities;
using Core.Entities.Tests;
using Core.QueryFilters;

namespace Infraestructure.Mappings
{
    public class TestAutomapperProfile : Profile
    {
        public TestAutomapperProfile()
        {
            CreateMap<TestCreateDto, TestEntity>();

            CreateMap<TestQueryFilterDto, TestQueryFilter>();

            // Unpopulated
            CreateMap<TestEntity, TestDto>()
                .IncludeBase<BaseEntity, BaseDto>();

            CreateMap<BaseQuestionEntity, BaseQuestionDto>()
                .IncludeBase<BaseEntity, BaseDto>();

            CreateMap<TestWithQuestions, TestDto>()
                .IncludeBase<TestEntity, TestDto>();

            // Populated
            CreateMap<TestEntity, PopulatedTestDto>()
                .IncludeBase<BaseEntity, BaseDto>();

            CreateMap<BaseQuestionEntity, BasePopulatedQuestionDto>()
                .IncludeBase<BaseEntity, BaseDto>();

            CreateMap<QuestionOptionVideoToWordEntity, QuestionOptionVideoToWordDto>()
                .IncludeBase<BaseQuestionEntity, BasePopulatedQuestionDto>();

            CreateMap<QuestionOptionWordToVideoEntity, QuestionOptionWordToVideoDto>()
                .IncludeBase<BaseQuestionEntity, BasePopulatedQuestionDto>();

            CreateMap<QuestionQAEntity, QuestionQADto>()
                .IncludeBase<BaseQuestionEntity, BasePopulatedQuestionDto>();

            CreateMap<QuestionMimicEntity, QuestionMimicDto>()
                .IncludeBase<BaseQuestionEntity, BasePopulatedQuestionDto>();
        }
    }
}