using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

using WCRM.SERVICESS.SHARED;
using WCRM.ViewModel;



namespace WCRM.Controllers.Main
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }


        public IActionResult Index()
        {
            #region
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion         
            var viewModel = new HomeViewModel
            {
                Services = GetServices(connString),
                Products = GetProducts(connString),
                sliders = GetSliders(connString)
            };
            

            return View(viewModel);
          
        }
        public List<Slider> GetSliders(string connString)
        {
            List<Slider> sliders = new List<Slider>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select Id, title,description,slider_image from sliders where status='Published'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sliders.Add(new Slider
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Title = reader["title"].ToString(),
                        Description = reader["description"].ToString(),
                        SliderImage = reader["slider_image"].ToString()
                    });
                }
            }

            return sliders;
        }
        public List<Product> GetProducts(string connString)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select Id, name,description from products";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt16(reader["Id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString()//,
                        //Icon = reader["icon"].ToString()
                    });
                }
            }

            return products;
        }
        public List<Service> GetServices(string connString)
        {
            List<Service> services = new List<Service>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Id, Name, Description, icon, Status, SEO_Title, SEO_Description, SEO_Keywords, created_at, updated_at   FROM services where Status='Published'   ORDER BY created_at DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    services.Add(new Service
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Icon = reader["icon"].ToString()
                    });
                }
            }

            return services;
        }
        public IActionResult Company()
        {
            #region
            //string connString = string.Empty;
            //connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            //DBConnectionServices.DatabaseConn(connString);
            #endregion

            return View();
        }
        public IActionResult Services()
        {
            #region
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion
            List<Service> services = GetServices(connString);
            return View(services);
        }
        public IActionResult Clients()
        {
            

            return View();
        }
        public IActionResult Technology()
        {
            

            return View();
        }
        public IActionResult Industries()
        {
            

            return View();
        }
        public IActionResult Contact()
        {
            

            return View();
        }
      
        public IActionResult Privacy()
        {
            #region
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion
            List<Privacy> privacys = GetPrivacy(connString);
            return View(privacys);
        }
        public List<Privacy> GetPrivacy(string connString)
        {
            List<Privacy> privacys = new List<Privacy>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Id, Name, Description, icon, Status, SEO_Title, SEO_Description, SEO_Keywords, created_at, updated_at   FROM privacy where Status='Published'   ORDER BY created_at DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    privacys.Add(new Privacy
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Icon = reader["icon"].ToString()
                    });
                }
            }

            return privacys;
        }
    }
}
