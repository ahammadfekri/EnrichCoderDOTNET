using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRM.MODEL.AdminPanel
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; }
        //SEO Info
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
    }
}
