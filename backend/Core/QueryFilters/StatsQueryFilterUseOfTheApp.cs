using System;
using System.ComponentModel.DataAnnotations;

namespace Core.QueryFilters
{
    public class StatsQueryFilterUseOfTheApp
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }
    }
}
