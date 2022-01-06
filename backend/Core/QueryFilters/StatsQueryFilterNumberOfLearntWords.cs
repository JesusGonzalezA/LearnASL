using System;
using System.ComponentModel.DataAnnotations;

namespace Core.QueryFilters
{
    public class StatsQueryFilterNumberOfLearntWords
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }
    }
}
