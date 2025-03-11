using Microsoft.AspNetCore.Mvc;
using WCRM.ADMIN.Models;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class WebsiteConfigController : Controller
    {
        private readonly WebsiteConfig_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WebsiteConfigController(WebsiteConfig_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: /WebsiteConfig/
        public IActionResult Index()
        {
            List<WebsiteConfig> configs = _dbHelper.GetAllConfigs();
            return View(configs);
        }
    }
}
