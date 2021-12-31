using Core.Enums;

namespace Core.Entities
{
    public class VideoEntity : BaseEntity
    {
        public int Index { get; set; }
        public string Word { get; set; }
        public string VideoFilename { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
