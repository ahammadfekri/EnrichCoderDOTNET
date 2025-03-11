using Microsoft.AspNetCore.Mvc;

namespace WCRM.ADMIN.Controllers
{
    public class OurTeamController : Controller
    {
        // GET: /OurTeam/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /OurTeam/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: /OurTeam/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.TeamMemberId = id;
            return View();
        }
    }
}
