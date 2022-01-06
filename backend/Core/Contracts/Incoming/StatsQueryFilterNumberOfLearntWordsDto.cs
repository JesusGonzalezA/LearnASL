using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.Incoming
{
    public class StatsQueryFilterNumberOfLearntWordsDto
    {
        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }
    }
}
