using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class NewsLatter
    {
        [Key]
        public  int Id { get; set; }
        public string Email { get; set; }
    }
}
