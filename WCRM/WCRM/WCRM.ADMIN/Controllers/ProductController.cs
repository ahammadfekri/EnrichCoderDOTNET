using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class ProductController : Controller
    {
        private readonly Product_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly string _sharedFolderPath;
        public ProductController(Product_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
            //_sharedFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "/wwwroot/uploads");
        }


        public IActionResult Index()
        {
            List<Product> Product = _dbHelper.GetAllProducts();
            return View(Product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model, IFormFile ImageFile)
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

                model.Icon = "/uploads/" + uniqueFileName; // URL for display
                model.Status = model.Status ?? "Unpublished";
            }

            if (model != null)
            {
                _dbHelper.AddProduct(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }





        public IActionResult Edit(int id)
        {
            var privacies = _dbHelper.GetProductById(id);
            if (privacies == null)
            {
                return NotFound();
            }
            return View(privacies);
        }

        [HttpPost]
        public IActionResult Edit(Product model, IFormFile ImageFile)
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
                var existingProduct = _dbHelper.GetProductById(model.Id);
                model.Icon = existingProduct.Icon;

            }
            if (model != null)
            {
                _dbHelper.UpdateProduct(model);
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
                _dbHelper.DeleteProduct(id);

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
