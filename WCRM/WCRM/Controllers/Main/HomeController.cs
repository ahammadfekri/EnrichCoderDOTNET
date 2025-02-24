using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WCRM.Models;
using WCRM.SERVICESS.HRMS;
using WCRM.SERVICESS.SHARED;



namespace WCRM.Controllers.Main
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            #region
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion         
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
