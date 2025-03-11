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
        public IActionResult Edit(int id)
        {
            var configs = _dbHelper.GetAllConfigsById(id);
            if (configs == null)
            {
                return NotFound();
            }
            return View(configs);
        }
        [HttpPost]
        public IActionResult Edit(WebsiteConfig model, IFormFile ImageFile)
        {
            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // ✅ Path to WCRM (frontend) wwwroot/uploads using relative path
                string frontendUploadsFolder = Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "WCRM", "wwwroot", "uploads")
                );

                // ✅ Create uploads folder if it doesn't exist
                if (!Directory.Exists(frontendUploadsFolder))
                {
                    Directory.CreateDirectory(frontendUploadsFolder);
                }

                // ✅ Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(frontendUploadsFolder, uniqueFileName);

                // ✅ Save the file to WCRM (frontend)
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // ✅ Ensure rich text is stored properly
                model.Description = model.Description ?? "";

                // ✅ Set the URL relative path for frontend access
                model.logo = "/uploads/" + uniqueFileName;
            }
            else
            {
                // ✅ Keep existing image URL if no new image is uploaded
                //var existingSlider = _dbHelper.GetSliderById(model.Id);
                //model.SliderImage = existingSlider.SliderImage;
            }

            // ✅ Update in database if model is valid
            if (model != null)
            {
                _dbHelper.UpdateWebsiteConfig(model);
                return RedirectToAction("Index");
            }

            // Return view with model in case of validation issues
            return View(model);
        }
    }
}
