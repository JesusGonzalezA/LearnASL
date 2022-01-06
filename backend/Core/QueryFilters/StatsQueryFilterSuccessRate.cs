using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.QueryFilters
{
    public class StatsQueryFilterSuccessRate
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}
