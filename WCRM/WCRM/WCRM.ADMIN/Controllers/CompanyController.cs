using Microsoft.AspNetCore.Mvc;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class CompanyController : Controller
    {
        private readonly Company_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(Company_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Company> companies = _dbHelper.GetAllCompanies();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company model, IFormFile ImageFile)
        {

            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // ✅ Path to WCRM Frontend's wwwroot/uploads using relative path
                string frontendUploadsFolder = Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "WCRM", "WCRM", "wwwroot", "uploads")
                );

                // ✅ Path to WCRM.ADMIN's wwwroot/uploads
                string adminUploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                // ✅ Ensure both folders exist
                if (!Directory.Exists(frontendUploadsFolder))
                    Directory.CreateDirectory(frontendUploadsFolder);

                if (!Directory.Exists(adminUploadsFolder))
                    Directory.CreateDirectory(adminUploadsFolder);

                // ✅ Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);

                // ✅ File paths
                string frontendFilePath = Path.Combine(frontendUploadsFolder, uniqueFileName);
                string adminFilePath = Path.Combine(adminUploadsFolder, uniqueFileName);

                // ✅ Save file to frontend (WCRM)
                using (var stream = new FileStream(frontendFilePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                // ✅ Copy file to Admin (WCRM.ADMIN)
                System.IO.File.Copy(frontendFilePath, adminFilePath, true); // Overwrite if exists

                model.Icon = "/uploads/" + uniqueFileName; // URL for display
                model.Status = model.Status ?? "Unpublished";
            }

            if (model != null)
            {
                _dbHelper.AddCompany(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }





        public IActionResult Edit(int id)
        {
            var Company = _dbHelper.GetCompanyById(id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        [HttpPost]
        public IActionResult Edit(Company model, IFormFile ImageFile)
        {
            // Handle Image Upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // ✅ Path to WCRM (frontend) wwwroot/uploads using relative path
                string frontendUploadsFolder = Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "WCRM", "wwwroot", "uploads")
                );
                // ✅ Path to WCRM.ADMIN's wwwroot/uploads
                string adminUploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                // ✅ Ensure both folders exist
                if (!Directory.Exists(frontendUploadsFolder))
                    Directory.CreateDirectory(frontendUploadsFolder);

                if (!Directory.Exists(adminUploadsFolder))
                    Directory.CreateDirectory(adminUploadsFolder);

                // ✅ Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(frontendUploadsFolder, uniqueFileName);
                // ✅ File paths
                string frontendFilePath = Path.Combine(frontendUploadsFolder, uniqueFileName);
                string adminFilePath = Path.Combine(adminUploadsFolder, uniqueFileName);
                // ✅ Save the file to WCRM (frontend)
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
                // ✅ Copy file to Admin (WCRM.ADMIN)
                System.IO.File.Copy(frontendFilePath, adminFilePath, true); // Overwrite if exists

                // Ensure rich text is stored properly
                model.Description = model.Description ?? "";

                // Update model with new image URL
                model.Icon = "/uploads/" + uniqueFileName;

            }
            else
            {
                // Keep existing image URL if no new image is uploaded
                var existingCompany = _dbHelper.GetCompanyById(model.Id);
                model.Icon = existingCompany.Icon;

            }
            if (model != null)
            {
                _dbHelper.UpdateCompany(model);
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
                _dbHelper.DeleteCompany(id);

                // Redirect to the Index page after successful deletion
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., project not found)
                ModelState.AddModelError("", "Error deleting Company: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
