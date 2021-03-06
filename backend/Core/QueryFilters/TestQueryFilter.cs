using System;
using Core.Enums;

namespace Core.QueryFilters
{
    public class TestQueryFilter
    {
        public Guid UserId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public TestType? TestType { get; set; }

        public Difficulty? Difficulty { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
