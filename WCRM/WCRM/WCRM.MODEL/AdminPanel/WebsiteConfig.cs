using Microsoft.AspNetCore.Http;

namespace WCRM.MODEL.AdminPanel
{
    public class WebsiteConfig
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string logo { get; set; }
        public IFormFile ImageFile { get; set; }

        public string Status { get; set; }
        //SEO Info
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AboutUs { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? LinkedIn { get; set; }
        public string? YouTube { get; set; }
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public string? WeChat { get; set; }
        public string? Skype { get; set; }
        public string? Snapchat { get; set; }
        public string? TikTok { get; set; }
        public string? Pinterest { get; set; }
        public string? Reddit { get; set; }
        public string? Vimeo { get; set; }
        public string? Quora { get; set; }
        public string? MySpace { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
