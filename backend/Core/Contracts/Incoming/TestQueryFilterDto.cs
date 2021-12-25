using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class TestQueryFilterDto
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public TestType? TestType { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}
