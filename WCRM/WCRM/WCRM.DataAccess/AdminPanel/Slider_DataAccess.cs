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
  
    public class Slider_DataAccess
    {

        private readonly string _connectionString;

        public Slider_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        

        public List<Slider> GetAllSliders()
        {
            var sliders = new List<Slider>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllSliders", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sliders.Add(new Slider
                            {
                                Id = reader.GetInt64(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                SliderImage = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Status = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                SEO_Title = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                SEO_Description = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                SEO_Keywords = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                CreatedAt = reader.GetDateTime(8),
                                UpdatedAt = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9)
                                //Id = reader.GetInt64(reader.GetOrdinal("Id")),
                                //Title = reader.GetString(reader.GetOrdinal("Title")),
                                //Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                                //SliderImage = reader.IsDBNull(reader.GetOrdinal("slider_image")) ? null : reader.GetString(reader.GetOrdinal("slider_image")),
                                //Status = reader.GetString(reader.GetOrdinal("Status")),
                                //CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                //UpdatedAt = reader.IsDBNull(reader.GetOrdinal("updated_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                //SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
                                //SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
                                //SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords"))

                            });
                        }
                    }
                }
            }

            return sliders;
        }

        // Get project by ID
        public Slider GetSliderById(long id)
        {
            Slider slider = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetSliderById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            slider = new Slider
                            {
                                Id = reader.GetInt64(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                SliderImage = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Status = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                SEO_Title = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                SEO_Description = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                SEO_Keywords = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                CreatedAt = reader.GetDateTime(8),
                                UpdatedAt = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9)
                            };
                        }
                    }
                }
            }

            return slider;
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
