using System.ComponentModel.DataAnnotations;

namespace GamzeBlogPsikolog.Entity
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        public string? SliderBigText { get; set; }
        public string? SliderSmallText { get; set; }
        public bool Status { get; set; }
        public string SliderImage { get; set; }
    }
}
