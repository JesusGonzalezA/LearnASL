using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Contracts.Incoming
{
    public class StatsQueryFilterSuccessRateDto
    {
        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}
