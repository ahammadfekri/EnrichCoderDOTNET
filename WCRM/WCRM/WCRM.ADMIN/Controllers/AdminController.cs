using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WCRM.ADMIN.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
