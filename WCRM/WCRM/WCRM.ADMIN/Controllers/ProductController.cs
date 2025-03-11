using Microsoft.AspNetCore.Mvc;

namespace WCRM.ADMIN.Controllers
{
    public class ProductController : Controller
    {
        // GET: /Product/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
    }
}
