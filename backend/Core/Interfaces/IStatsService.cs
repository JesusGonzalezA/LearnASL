using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.QueryFilters;

namespace Core.Interfaces
{
    public interface IStatsService
    {
        IEnumerable<int> GetMonthlyUseOfTheAppByUser(StatsQueryFilterUseOfTheApp filter);
        int GetNumberOfWordsLearntByUser(Guid userId, StatsQueryFilterNumberOfLearntWords filter);
        Task<double> GetPercentOfWordsLearntByUser(Guid userId);
        int GetBestStreak(Guid userId);
        int GetCurrentStreak(Guid userId);
    }
}
