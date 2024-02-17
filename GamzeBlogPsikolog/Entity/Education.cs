using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string? Name { get; set; }
        public bool Statu { get; set; }
    }
}
