using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRM.MODEL.AdminPanel
{
    public class HeroSectionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string BackgroundImage { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }


        // SEO Fields
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
        public string Status { get; set; }
    }
}
