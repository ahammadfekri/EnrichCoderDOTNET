using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.SHARED;
using WCRM.MODEL.AdminPanel;

namespace WCRM.DataAccess.AdminPanel
{

    public class WebsiteConfig_DataAccess
    {

        private readonly string _connectionString;

        public WebsiteConfig_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<WebsiteConfig> GetAllConfigs()
        {
            var configs = new List<WebsiteConfig>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllWebsiteConfig", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                configs.Add(new WebsiteConfig
                                {
                                    Id = reader.GetInt64(reader.GetOrdinal("id")),
                                    Title = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("about_us")) ? "" : reader.GetString(reader.GetOrdinal("about_us")),
                                    logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : reader.GetString(reader.GetOrdinal("logo")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? "N/A" : reader.GetString(reader.GetOrdinal("Status")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? "" : reader.GetString(reader.GetOrdinal("phone")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("address")) ? "" : reader.GetString(reader.GetOrdinal("address")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),// Not in SELECT query, add if needed
                                    SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
                                    SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
                                    SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords")),

                                    Facebook = reader.IsDBNull(reader.GetOrdinal("facebook")) ? "N/A" : reader.GetString(reader.GetOrdinal("facebook")),
                                    Twitter = reader.IsDBNull(reader.GetOrdinal("twitter")) ? "N/A" : reader.GetString(reader.GetOrdinal("twitter")),
                                    LinkedIn = reader.IsDBNull(reader.GetOrdinal("linkedin")) ? "N/A" : reader.GetString(reader.GetOrdinal("linkedin")),
                                    YouTube = reader.IsDBNull(reader.GetOrdinal("youtube")) ? "N/A" : reader.GetString(reader.GetOrdinal("youtube")),
                                    Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "N/A" : reader.GetString(reader.GetOrdinal("instagram")),
                                    Telegram = reader.IsDBNull(reader.GetOrdinal("telegram")) ? "N/A" : reader.GetString(reader.GetOrdinal("telegram")),
                                    WhatsApp = reader.IsDBNull(reader.GetOrdinal("whatsapp")) ? "N/A" : reader.GetString(reader.GetOrdinal("whatsapp")),
                                    WeChat = reader.IsDBNull(reader.GetOrdinal("wechat")) ? "N/A" : reader.GetString(reader.GetOrdinal("wechat")),
                                    Skype = reader.IsDBNull(reader.GetOrdinal("skype")) ? "N/A" : reader.GetString(reader.GetOrdinal("skype")),
                                    Snapchat = reader.IsDBNull(reader.GetOrdinal("snapchat")) ? "N/A" : reader.GetString(reader.GetOrdinal("snapchat")),
                                    TikTok = reader.IsDBNull(reader.GetOrdinal("tiktok")) ? "N/A" : reader.GetString(reader.GetOrdinal("tiktok")),
                                    Pinterest = reader.IsDBNull(reader.GetOrdinal("pinterest")) ? "N/A" : reader.GetString(reader.GetOrdinal("pinterest")),
                                    Reddit = reader.IsDBNull(reader.GetOrdinal("reddit")) ? "N/A" : reader.GetString(reader.GetOrdinal("reddit")),
                                    Vimeo = reader.IsDBNull(reader.GetOrdinal("vimeo")) ? "N/A" : reader.GetString(reader.GetOrdinal("vimeo")),
                                    Quora = reader.IsDBNull(reader.GetOrdinal("quora")) ? "N/A" : reader.GetString(reader.GetOrdinal("quora")),
                                    MySpace = reader.IsDBNull(reader.GetOrdinal("myspace")) ? "N/A" : reader.GetString(reader.GetOrdinal("myspace"))

                                });
                            }

                        }
                        else
                        {

                        }

                    }
                }
            }

            return configs;
        }




        // Get project by ID
        public WebsiteConfig GetAllConfigsById(int id)
        {
            WebsiteConfig config = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllWebsiteConfig", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            config = new WebsiteConfig
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("id")),
                                Title = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("about_us")) ? "" : reader.GetString(reader.GetOrdinal("about_us")),
                                logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : reader.GetString(reader.GetOrdinal("logo")),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? "N/A" : reader.GetString(reader.GetOrdinal("Status")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? "" : reader.GetString(reader.GetOrdinal("phone")),
                                Address = reader.IsDBNull(reader.GetOrdinal("address")) ? "" : reader.GetString(reader.GetOrdinal("address")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),// Not in SELECT query, add if needed
                                SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
                                SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
                                SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords")),

                                Facebook = reader.IsDBNull(reader.GetOrdinal("facebook")) ? "N/A" : reader.GetString(reader.GetOrdinal("facebook")),
                                Twitter = reader.IsDBNull(reader.GetOrdinal("twitter")) ? "N/A" : reader.GetString(reader.GetOrdinal("twitter")),
                                LinkedIn = reader.IsDBNull(reader.GetOrdinal("linkedin")) ? "N/A" : reader.GetString(reader.GetOrdinal("linkedin")),
                                YouTube = reader.IsDBNull(reader.GetOrdinal("youtube")) ? "N/A" : reader.GetString(reader.GetOrdinal("youtube")),
                                Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? "N/A" : reader.GetString(reader.GetOrdinal("instagram")),
                                Telegram = reader.IsDBNull(reader.GetOrdinal("telegram")) ? "N/A" : reader.GetString(reader.GetOrdinal("telegram")),
                                WhatsApp = reader.IsDBNull(reader.GetOrdinal("whatsapp")) ? "N/A" : reader.GetString(reader.GetOrdinal("whatsapp")),
                                WeChat = reader.IsDBNull(reader.GetOrdinal("wechat")) ? "N/A" : reader.GetString(reader.GetOrdinal("wechat")),
                                Skype = reader.IsDBNull(reader.GetOrdinal("skype")) ? "N/A" : reader.GetString(reader.GetOrdinal("skype")),
                                Snapchat = reader.IsDBNull(reader.GetOrdinal("snapchat")) ? "N/A" : reader.GetString(reader.GetOrdinal("snapchat")),
                                TikTok = reader.IsDBNull(reader.GetOrdinal("tiktok")) ? "N/A" : reader.GetString(reader.GetOrdinal("tiktok")),
                                Pinterest = reader.IsDBNull(reader.GetOrdinal("pinterest")) ? "N/A" : reader.GetString(reader.GetOrdinal("pinterest")),
                                Reddit = reader.IsDBNull(reader.GetOrdinal("reddit")) ? "N/A" : reader.GetString(reader.GetOrdinal("reddit")),
                                Vimeo = reader.IsDBNull(reader.GetOrdinal("vimeo")) ? "N/A" : reader.GetString(reader.GetOrdinal("vimeo")),
                                Quora = reader.IsDBNull(reader.GetOrdinal("quora")) ? "N/A" : reader.GetString(reader.GetOrdinal("quora")),
                                MySpace = reader.IsDBNull(reader.GetOrdinal("myspace")) ? "N/A" : reader.GetString(reader.GetOrdinal("myspace"))
                            };

                        }
                    }
                }
            }

            return config;
        }



        // Update project
        public void UpdateWebsiteConfig(WebsiteConfig configs)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("SP_InsertUpdateWebsiteSettings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", configs.Id);
                    command.Parameters.AddWithValue("@name", configs.Title);
                    command.Parameters.AddWithValue("@logo", configs.logo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", configs.Email);
                    command.Parameters.AddWithValue("@phone", configs.Phone);
                    command.Parameters.AddWithValue("@address", configs.Address);
                    command.Parameters.AddWithValue("@about_us", configs.Description);
                    command.Parameters.AddWithValue("@facebook", configs.Facebook ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@twitter", configs.Twitter ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@linkedin", configs.LinkedIn ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@youtube", configs.YouTube ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@instagram", configs.Instagram ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telegram", configs.Telegram ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@whatsapp", configs.WhatsApp ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@wechat", configs.WeChat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@skype", configs.Skype ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@snapchat", configs.Snapchat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@tiktok", configs.TikTok ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pinterest", configs.Pinterest ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reddit", configs.Reddit ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@vimeo", configs.Vimeo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@quora", configs.Quora ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@myspace", configs.MySpace ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", configs.Status);
                    command.Parameters.AddWithValue("@SEO_Title", configs.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", configs.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", configs.SEO_Keywords);


                    command.ExecuteNonQuery();
                }
            }
        }
        // Delete a project using stored procedure
        public void DeleteSlider(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteSliders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
