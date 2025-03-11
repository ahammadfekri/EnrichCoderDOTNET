using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WCRM.SERVICESS.AdminPanel;
using WCRM.MODEL.AdminPanel;
using WCRM.SERVICESS.SHARED;
using System.Security.Cryptography;
using System.Text;
using WCRM.DataAccess.AdminPanel;

namespace WCRM.ADMIN.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration Configuration;
        private readonly User_DataAccess _userDataAccess;

        public AccountController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            _userDataAccess = new User_DataAccess();
        }

        // ✅ Establish DB Connection
        private void SetDatabaseConnection()
        {
            string connString = Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
        }

        // ✅ GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            SetDatabaseConnection();
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            SetDatabaseConnection();

            if (ModelState.IsValid)
            {
                // Generate Salt & Hash Password
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(model.PasswordHash, salt);

                Console.WriteLine("Generated Salt: " + salt);
                Console.WriteLine("Generated Hashed Password: " + hashedPassword);

                model.PasswordHash = hashedPassword;
                model.Salt = salt;

                bool isUserCreated = _userDataAccess.CreateUser(model);
                if (isUserCreated)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Error creating user. Username might already exist.";
                }
            }
            return View(model);
        }


        // ✅ GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            SetDatabaseConnection();
            return View();
        }

        // ✅ POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            SetDatabaseConnection();
            UserModel user = _userDataAccess.GetUserByUsername(username);

            if (user != null)
            {
                // Validate the entered password with stored hash
                string hashedInputPassword = HashPassword(password, user.Salt);
                if (hashedInputPassword == user.PasswordHash)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Admin"); // Redirect after successful login
                }
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }

        // ✅ Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // ✅ Generate Salt
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // ✅ Hash Password using SHA-256
        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = passwordBytes.Concat(saltBytes).ToArray();

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
