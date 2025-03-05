using Microsoft.AspNetCore.Mvc;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class HeroSectionController : Controller
    {
        private readonly HeroSection_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HeroSectionController(HeroSection_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ 1. List all Hero Sections
        public IActionResult Index()
        {
            List<HeroSectionModel> heroSections = _dbHelper.GetAllHeroSections();
            return View(heroSections);
        }

        // ✅ 2. Create Hero Section - GET
        public IActionResult Create()
        {
            return View();
        }

        // ✅ 3. Create Hero Section - POST
        [HttpPost]
        public IActionResult Create(HeroSectionModel model, IFormFile BackgroundImageFile)
        {
            // Handle Background Image Upload
            if (BackgroundImageFile != null && BackgroundImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                // Create 'uploads' folder if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(BackgroundImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BackgroundImageFile.CopyTo(stream);
                }

                model.BackgroundImage = "/uploads/" + uniqueFileName; // URL for display
                model.Status = model.Status ?? "Unpublished";
            }

            if (model != null)
            {
                _dbHelper.AddHeroSection(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // ✅ 4. Edit Hero Section - GET
        public IActionResult Edit(int id)
        {
            var heroSection = _dbHelper.GetHeroSectionById(id);
            if (heroSection == null)
            {
                return NotFound();
            }
            return View(heroSection);
        }

        // ✅ 5. Edit Hero Section - POST
        [HttpPost]
        public IActionResult Edit(HeroSectionModel model, IFormFile BackgroundImageFile)
        {
            // Handle Background Image Upload
            if (BackgroundImageFile != null && BackgroundImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(BackgroundImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BackgroundImageFile.CopyTo(stream);
                }

                // Update model with new image URL
                model.BackgroundImage = "/uploads/" + uniqueFileName;
            }
            else
            {
                // Keep existing image URL if no new image is uploaded
                var existingHeroSection = _dbHelper.GetHeroSectionById(model.Id);
                model.BackgroundImage = existingHeroSection.BackgroundImage;
            }

            if (model != null)
            {
                _dbHelper.UpdateHeroSection(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // ✅ 6. Delete Hero Section
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                // Call data access to delete the hero section
                _dbHelper.DeleteHeroSection(id);

                // Redirect to Index page after successful deletion
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., hero section not found)
                ModelState.AddModelError("", "Error deleting hero section: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
