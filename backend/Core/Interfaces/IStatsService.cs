using System.Collections.Generic;
using Core.QueryFilters;

namespace Core.Interfaces
{
    public interface IStatsService
    {
        IEnumerable<int> GetMonthlyUseOfTheAppByUser(StatsQueryFilterUseOfTheApp filter);
    }
}
