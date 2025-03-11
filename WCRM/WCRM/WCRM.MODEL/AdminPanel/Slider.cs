using Microsoft.AspNetCore.Http;

namespace WCRM.MODEL.AdminPanel
{
    public class Slider
    {
        public long Id { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  string SliderImage { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; }
        //SEO Info
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
    }
}
