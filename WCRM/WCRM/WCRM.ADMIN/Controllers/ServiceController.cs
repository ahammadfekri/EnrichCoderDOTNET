using Microsoft.AspNetCore.Mvc;
using WCRM.ADMIN.Models;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class ServiceController : Controller
    {
        private readonly Service_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServiceController(Service_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Service> services = _dbHelper.GetAllServices();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service model, IFormFile ImageFile)
        {

            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                // Create 'uploads' folder if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                model.Icon = "/uploads/" + uniqueFileName; // URL for display
                model.Status = model.Status ?? "Unpublished";
            }

            if (model != null)
            {
                _dbHelper.AddService(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }





        public IActionResult Edit(int id)
        {
            var service = _dbHelper.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service model, IFormFile ImageFile)
        {
            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // Ensure rich text is stored properly
                model.Description = model.Description ?? "";

                // Update model with new image URL
                model.Icon = "/uploads/" + uniqueFileName;

            }
            else
            {
                // Keep existing image URL if no new image is uploaded
                var existingSerivce = _dbHelper.GetServiceById(model.Id);
                model.Icon = existingSerivce.Icon;

            }
            if (model != null)
            {
                _dbHelper.UpdateService(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                // Call data access to delete the project
                _dbHelper.DeleteService(id);

                // Redirect to the Index page after successful deletion
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., project not found)
                ModelState.AddModelError("", "Error deleting project: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
