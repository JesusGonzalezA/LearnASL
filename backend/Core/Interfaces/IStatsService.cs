using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.QueryFilters;

namespace Core.Interfaces
{
    public interface IStatsService
    {
        IEnumerable<int> GetMonthlyUseOfTheAppByUser(StatsQueryFilterUseOfTheApp filter);
        Task<int> GetNumberOfWordsLearntByUser(Guid userId);
        Task<double> GetPercentOfWordsLearntByUser(Guid userId);
        int GetBestStreak(Guid userId);
        int GetCurrentStreak(Guid userId);
    }
}
