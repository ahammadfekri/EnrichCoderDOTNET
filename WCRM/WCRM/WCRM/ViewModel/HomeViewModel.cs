using WCRM.MODEL.AdminPanel;

namespace WCRM.ViewModel
{
    public class HomeViewModel
    {
        public List<Service> Services { get; set; }
        public List<Product> Products { get; set; }
        public List<Slider> sliders { get; set; }
        public WebsiteConfig configs { get; set; }
        public List<Privacy> privacy { get; set; }
        public List<Company> company { get; set; }
        public List<Industry> industries { get; set; }
    }
}
