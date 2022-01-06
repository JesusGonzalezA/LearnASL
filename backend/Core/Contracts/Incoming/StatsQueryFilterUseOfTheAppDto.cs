using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.Incoming
{
    public class StatsQueryFilterUseOfTheAppDto
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }
    }
}
