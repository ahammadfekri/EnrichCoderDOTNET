using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WCRM.SERVICESS.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class AccountController : Controller
    {
        User_Service objUserService;
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();  // This renders the Login.cshtml view
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            objUserService = new User_Service();
            UserModel ob = new UserModel();
            ob.Username = username;
            ob.PasswordHash = password;
            if (objUserService.ValidateUser(ob))  // Replace with DB validation
            {
                // Cookie authentication logic here
                return RedirectToAction("Index", "Home"); // Redirect after successful login
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }
    }
}
