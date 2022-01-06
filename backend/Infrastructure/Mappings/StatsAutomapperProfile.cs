using AutoMapper;
using Core.Contracts.Incoming;
using Core.QueryFilters;

namespace Infrastructure.Mappings
{
    public class StatsAutomapperProfile : Profile
    {
        public StatsAutomapperProfile()
        {
            CreateMap<StatsQueryFilterUseOfTheAppDto, StatsQueryFilterUseOfTheApp>();
            CreateMap<StatsQueryFilterNumberOfLearntWordsDto, StatsQueryFilterNumberOfLearntWords>();
        }
    }
}
