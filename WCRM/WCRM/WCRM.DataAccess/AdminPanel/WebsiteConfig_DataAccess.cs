using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.ADMIN.Models;
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
                        while (reader.Read())
                        {
                            configs.Add(new WebsiteConfig
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("about_us")) ? "" : reader.GetString(reader.GetOrdinal("about_us")),
                                logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : reader.GetString(reader.GetOrdinal("logo")),
                                Status = reader.GetString(reader.GetOrdinal("Status")), // Not in SELECT query, add if needed
                                SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
                                SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
                                SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                UpdatedAt = null, // Assuming no "updated_at" field
                                Facebook = reader.IsDBNull(reader.GetOrdinal("facebook")) ? null : reader.GetString(reader.GetOrdinal("facebook")),
                                Twitter = reader.IsDBNull(reader.GetOrdinal("twitter")) ? null : reader.GetString(reader.GetOrdinal("twitter")),
                                LinkedIn = reader.IsDBNull(reader.GetOrdinal("linkedin")) ? null : reader.GetString(reader.GetOrdinal("linkedin")),
                                YouTube = reader.IsDBNull(reader.GetOrdinal("youtube")) ? null : reader.GetString(reader.GetOrdinal("youtube")),
                                Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? null : reader.GetString(reader.GetOrdinal("instagram")),
                                Telegram = reader.IsDBNull(reader.GetOrdinal("telegram")) ? null : reader.GetString(reader.GetOrdinal("telegram")),
                                WhatsApp = reader.IsDBNull(reader.GetOrdinal("whatsapp")) ? null : reader.GetString(reader.GetOrdinal("whatsapp")),
                                WeChat = reader.IsDBNull(reader.GetOrdinal("wechat")) ? null : reader.GetString(reader.GetOrdinal("wechat")),
                                Skype = reader.IsDBNull(reader.GetOrdinal("skype")) ? null : reader.GetString(reader.GetOrdinal("skype")),
                                Snapchat = reader.IsDBNull(reader.GetOrdinal("snapchat")) ? null : reader.GetString(reader.GetOrdinal("snapchat")),
                                TikTok = reader.IsDBNull(reader.GetOrdinal("tiktok")) ? null : reader.GetString(reader.GetOrdinal("tiktok")),
                                Pinterest = reader.IsDBNull(reader.GetOrdinal("pinterest")) ? null : reader.GetString(reader.GetOrdinal("pinterest")),
                                Reddit = reader.IsDBNull(reader.GetOrdinal("reddit")) ? null : reader.GetString(reader.GetOrdinal("reddit")),
                                Vimeo = reader.IsDBNull(reader.GetOrdinal("vimeo")) ? null : reader.GetString(reader.GetOrdinal("vimeo")),
                                Quora = reader.IsDBNull(reader.GetOrdinal("quora")) ? null : reader.GetString(reader.GetOrdinal("quora")),
                                MySpace = reader.IsDBNull(reader.GetOrdinal("myspace")) ? null : reader.GetString(reader.GetOrdinal("myspace"))

                            });
                        }
                    }
                }
            }

            return configs;
        }




        // Get project by ID
        public WebsiteConfig GetWebsiteConfig()
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
                                Title = reader.GetString(reader.GetOrdinal("name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("about_us")) ? "" : reader.GetString(reader.GetOrdinal("about_us")),
                                logo = reader.IsDBNull(reader.GetOrdinal("logo")) ? null : reader.GetString(reader.GetOrdinal("logo")),
                                Status = "", // Not in SELECT query, add if needed
                                SEO_Title = "", // Not in SELECT query, add if needed
                                SEO_Description = "", // Not in SELECT query, add if needed
                                SEO_Keywords = "", // Not in SELECT query, add if needed
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                UpdatedAt = null, // Assuming no "updated_at" field
                                Facebook = reader.IsDBNull(reader.GetOrdinal("facebook")) ? null : reader.GetString(reader.GetOrdinal("facebook")),
                                Twitter = reader.IsDBNull(reader.GetOrdinal("twitter")) ? null : reader.GetString(reader.GetOrdinal("twitter")),
                                LinkedIn = reader.IsDBNull(reader.GetOrdinal("linkedin")) ? null : reader.GetString(reader.GetOrdinal("linkedin")),
                                YouTube = reader.IsDBNull(reader.GetOrdinal("youtube")) ? null : reader.GetString(reader.GetOrdinal("youtube")),
                                Instagram = reader.IsDBNull(reader.GetOrdinal("instagram")) ? null : reader.GetString(reader.GetOrdinal("instagram")),
                                Telegram = reader.IsDBNull(reader.GetOrdinal("telegram")) ? null : reader.GetString(reader.GetOrdinal("telegram")),
                                WhatsApp = reader.IsDBNull(reader.GetOrdinal("whatsapp")) ? null : reader.GetString(reader.GetOrdinal("whatsapp")),
                                WeChat = reader.IsDBNull(reader.GetOrdinal("wechat")) ? null : reader.GetString(reader.GetOrdinal("wechat")),
                                Skype = reader.IsDBNull(reader.GetOrdinal("skype")) ? null : reader.GetString(reader.GetOrdinal("skype")),
                                Snapchat = reader.IsDBNull(reader.GetOrdinal("snapchat")) ? null : reader.GetString(reader.GetOrdinal("snapchat")),
                                TikTok = reader.IsDBNull(reader.GetOrdinal("tiktok")) ? null : reader.GetString(reader.GetOrdinal("tiktok")),
                                Pinterest = reader.IsDBNull(reader.GetOrdinal("pinterest")) ? null : reader.GetString(reader.GetOrdinal("pinterest")),
                                Reddit = reader.IsDBNull(reader.GetOrdinal("reddit")) ? null : reader.GetString(reader.GetOrdinal("reddit")),
                                Vimeo = reader.IsDBNull(reader.GetOrdinal("vimeo")) ? null : reader.GetString(reader.GetOrdinal("vimeo")),
                                Quora = reader.IsDBNull(reader.GetOrdinal("quora")) ? null : reader.GetString(reader.GetOrdinal("quora")),
                                MySpace = reader.IsDBNull(reader.GetOrdinal("myspace")) ? null : reader.GetString(reader.GetOrdinal("myspace"))
                            };

                        }
                    }
                }
            }

            return config;
        }

        // Add a new project using stored procedure
        public void AddSlider(Slider slider)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddSlider", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@Title", slider.Title);
                    command.Parameters.AddWithValue("@Description", slider.Description);
                    command.Parameters.AddWithValue("@slider_image", slider.SliderImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", slider.Status ?? (object)DBNull.Value);

                    // SEO Fields (Fixing typo)
                    command.Parameters.AddWithValue("@SEO_Title", slider.SEO_Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Description", slider.SEO_Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Keywords", slider.SEO_Keywords ?? (object)DBNull.Value);


                    command.ExecuteNonQuery();
                }
            }
        }

        // Update project
        public void UpdateSlider(Slider slider)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateSliders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", slider.Id);
                    command.Parameters.AddWithValue("@Title", slider.Title);
                    command.Parameters.AddWithValue("@Description", slider.Description);
                    command.Parameters.AddWithValue("@slider_image", slider.SliderImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", slider.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Title", slider.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", slider.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", slider.SEO_Keywords);


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
