using Microsoft.AspNetCore.Mvc;

namespace WCRM.ADMIN.Controllers
{
    public class ClientController : Controller
    {
        // GET: /Client/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: /Client/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.ClientId = id;
            return View();
        }
    }
}
