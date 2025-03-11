using Microsoft.AspNetCore.Mvc;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly Project_DataAccess _dbHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PortfolioController(Project_DataAccess dbHelper, IWebHostEnvironment webHostEnvironment)
        {
            _dbHelper = dbHelper;
            _webHostEnvironment = webHostEnvironment;
        }
       

        public IActionResult Index()
        {
            List<ProjectModel> projects = _dbHelper.GetAllProjects();
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProjectModel model, IFormFile ImageFile)
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

                    model.ImageUrl = "/uploads/" + uniqueFileName; // URL for display
                    model.Status = model.Status ?? "Unpublished";
            }

            if (model != null)
            {
                _dbHelper.AddProject(model);
                return RedirectToAction("Index");
            }
            return View(model);
      }

            
        


        public IActionResult Edit(int id)
        {
            var project = _dbHelper.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(ProjectModel model, IFormFile ImageFile)
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
                model.ImageUrl = "/uploads/" + uniqueFileName;
                
            }
            else
            {
                // Keep existing image URL if no new image is uploaded
                var existingProject = _dbHelper.GetProjectById(model.Id);
                model.ImageUrl = existingProject.ImageUrl;
                
            }
            if(model != null)
            {
                _dbHelper.UpdateProject(model);
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
                _dbHelper.DeleteProject(id);

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
