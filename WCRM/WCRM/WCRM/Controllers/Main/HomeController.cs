using System.Data;
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString),
                company = GetCompany(connString),
                industries = GetIndustry(connString),
            };


            return View(viewModel);

        }
        public List<Industry> GetIndustry(string connString)
        {
            List<Industry> industries = new List<Industry>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select top 6 Id, name,description,icon from Industries  where Status='Published'   ORDER BY created_at DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    industries.Add(new Industry
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Icon = reader["icon"].ToString()
                    });
                }
            }

            return industries;
        }
        public WebsiteConfig GetConfigs(string connString)
        {
            WebsiteConfig configs = new WebsiteConfig();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "GetAllWebsiteConfig";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    //configs.Add(new WebsiteConfig
                    //{
                    configs.Id = reader.GetInt64(reader.GetOrdinal("id"));
                    configs.Title = reader.GetString(reader.GetOrdinal("name"));
                    configs.Description = reader.IsDBNull(reader.GetOrdinal("about_us")) ? "" : reader.GetString(reader.GetOrdinal("about_us"));
                    configs.logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : reader.GetString(reader.GetOrdinal("logo"));
                    configs.Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email"));
                    configs.Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address"));
                    configs.Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone"));

                    configs.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? "N/A" : reader.GetString(reader.GetOrdinal("Status")); // Not in SELECT query, add if needed
                    configs.SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title"));
                    configs.SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description"));
                    configs.SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords"));
                    configs.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                    configs.UpdatedAt = null; // Assuming no "updated_at" field
                    configs.Facebook = reader.IsDBNull(reader.GetOrdinal("facebook")) ? "N/A" : reader.GetString(reader.GetOrdinal("facebook"));
                    configs.Twitter = reader.IsDBNull(reader.GetOrdinal("twitter")) ? "N/A" : reader.GetString(reader.GetOrdinal("twitter"));
                    configs.LinkedIn = reader.IsDBNull(reader.GetOrdinal("linkedin")) ? "N/A" : reader.GetString(reader.GetOrdinal("linkedin"));
                    configs.YouTube = reader.IsDBNull(reader.GetOrdinal("youtube")) ? "N/A" : reader.GetString(reader.GetOrdinal("youtube"));
                    configs.Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "N/A" : reader.GetString(reader.GetOrdinal("instagram"));
                    configs.Telegram = reader.IsDBNull(reader.GetOrdinal("telegram")) ? "N/A" : reader.GetString(reader.GetOrdinal("telegram"));
                    configs.WhatsApp = reader.IsDBNull(reader.GetOrdinal("whatsapp")) ? "N/A" : reader.GetString(reader.GetOrdinal("whatsapp"));
                    configs.WeChat = reader.IsDBNull(reader.GetOrdinal("wechat")) ? "N/A" : reader.GetString(reader.GetOrdinal("wechat"));
                    configs.Skype = reader.IsDBNull(reader.GetOrdinal("skype")) ? "N/A" : reader.GetString(reader.GetOrdinal("skype"));
                    configs.Snapchat = reader.IsDBNull(reader.GetOrdinal("snapchat")) ? "N/A" : reader.GetString(reader.GetOrdinal("snapchat"));
                    configs.TikTok = reader.IsDBNull(reader.GetOrdinal("tiktok")) ? "N/A" : reader.GetString(reader.GetOrdinal("tiktok"));
                    configs.Pinterest = reader.IsDBNull(reader.GetOrdinal("pinterest")) ? "N/A" : reader.GetString(reader.GetOrdinal("pinterest"));
                    configs.Reddit = reader.IsDBNull(reader.GetOrdinal("reddit")) ? "N/A" : reader.GetString(reader.GetOrdinal("reddit"));
                    configs.Vimeo = reader.IsDBNull(reader.GetOrdinal("vimeo")) ? "N/A" : reader.GetString(reader.GetOrdinal("vimeo"));
                    configs.Quora = reader.IsDBNull(reader.GetOrdinal("quora")) ? "N/A" : reader.GetString(reader.GetOrdinal("quora"));
                    configs.MySpace = reader.IsDBNull(reader.GetOrdinal("myspace")) ? "N/A" : reader.GetString(reader.GetOrdinal("myspace"));

                    configs.happyclient = reader.IsDBNull(reader.GetOrdinal("happyclient")) ? 0 : reader.GetInt32(reader.GetOrdinal("happyclient"));
                    configs.award = reader.IsDBNull(reader.GetOrdinal("award")) ? 0 : reader.GetInt32(reader.GetOrdinal("award"));
                    configs.projects = reader.IsDBNull(reader.GetOrdinal("projects")) ? 0 : reader.GetInt32(reader.GetOrdinal("projects"));
                    configs.team_member = reader.IsDBNull(reader.GetOrdinal("team_member")) ? 0 : reader.GetInt32(reader.GetOrdinal("team_member"));
                    //});
                }
            }

            return configs;
        }
        public List<Slider> GetSliders(string connString)
        {
            List<Slider> sliders = new List<Slider>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "select top 6 Id, title,description,slider_image from sliders where status='Published'   ORDER BY created_at DESC";
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
                string query = "select top 6 Id, name,description,icon from products  where Status='Published'   ORDER BY created_at DESC ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt16(reader["Id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Icon = reader["icon"].ToString()
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
                string query = "SELECT top 6 Id, Name, Description, icon, Status, SEO_Title, SEO_Description, SEO_Keywords, created_at, updated_at   FROM services where Status='Published'   ORDER BY created_at DESC";
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
            string connString = string.Empty;
            connString = this.Configuration.GetConnectionString("WeblinkDBConnection");
            DBConnectionServices.DatabaseConn(connString);
            #endregion

            var viewModel = new HomeViewModel
            {
                Services = GetServices(connString),
                Products = GetProducts(connString),
                sliders = GetSliders(connString),
                configs = GetConfigs(connString),
                privacy = GetPrivacy(connString),
                company = GetCompany(connString),
            };

            return View(viewModel);
        }
        public IActionResult Services()
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString)
            };

            return View(viewModel);
        }
        public IActionResult Clients()
        {


            return View();
        }
        public IActionResult Technology()
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString)
            };

            return View(viewModel);

            //return View();
        }
        public IActionResult Industries()
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString),
                industries = GetIndustry(connString),
            };

            return View(viewModel);
        }
        public IActionResult Contact()
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
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
                sliders = GetSliders(connString),
                configs = GetConfigs(connString),
                privacy = GetPrivacy(connString),
            };
            
            return View(viewModel);
           
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
        public List<Company> GetCompany(string connString)
        {
            List<Company> companies = new List<Company>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT top 1 Id, Name, Description, icon, Status, SEO_Title, SEO_Description, SEO_Keywords, created_at, updated_at   FROM Company where Status='Published'   ORDER BY created_at DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    companies.Add(new Company
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Icon = reader["icon"].ToString()
                    });
                }
            }

            return companies;
        }
    }
}
