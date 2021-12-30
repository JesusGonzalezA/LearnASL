using System;
using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class TestQueryFilterDto
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public TestType? TestType { get; set; }

        public Difficulty? Difficulty { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public TestQueryFilterDto()
        { }

        public TestQueryFilterDto(TestQueryFilterDto other)
        {
            PageSize = other.PageSize;
            PageNumber = other.PageNumber;
            TestType = other.TestType;
            Difficulty = other.Difficulty;
            FromDate = other.FromDate;
            ToDate = other.ToDate;
        }
    }
}
