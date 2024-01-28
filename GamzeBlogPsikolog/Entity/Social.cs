using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class Social
    {
        [Key]
        public int SocialId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Tiktok { get; set; }
    }
}
