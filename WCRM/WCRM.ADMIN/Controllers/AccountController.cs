using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WCRM.SERVICESS.AdminPanel;
using WCRM.MODEL.AdminPanel;
using WCRM.SERVICESS.SHARED;

namespace WCRM.ADMIN.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration Configuration;

        public AccountController(IConfiguration _configuration)
        {
            //_DB Connection;

            Configuration = _configuration;
        }
        User_Service objUserService;
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            #region
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion   
            return View();  // This renders the Login.cshtml view
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            objUserService = new User_Service();
            UserModel ob = new UserModel();
            ob.Username = username;
            ob.PasswordHash = password;
            if (objUserService.ValidateUser(ob))  // Replace with DB validation
            {
                // Cookie authentication logic here
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Admin"); // Redirect after successful login
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
