using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRM.MODEL.AdminPanel
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // For storing image path
        public IFormFile ImageFile { get; set; } // For uploading image
        public string Status { get; set; } // Published or Unpublished
        public string SEO_Title { get; set; }
        public string SEO_Description { get; set; }
        public string SEO_Keywords { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
