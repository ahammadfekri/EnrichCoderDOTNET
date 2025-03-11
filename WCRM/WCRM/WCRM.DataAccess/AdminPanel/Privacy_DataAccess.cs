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
  
    public class Privacy_DataAccess
    {

        private readonly string _connectionString;

        public Privacy_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Privacy> GetAllPrivacies()
        {
            var Privacies = new List<Privacy>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_GetAllPrivacy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Privacies.Add(new Privacy
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Icon = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Status = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                SEO_Title = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                SEO_Description = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                SEO_Keywords = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                CreatedAt = reader.GetDateTime(8),
                                UpdatedAt = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9)
                            });
                        }
                    }
                }
            }

            return Privacies;
        }


        public Privacy GetPrivacyById(long id)
        {
            Privacy Privacy = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_GetPrivacyById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Privacy = new Privacy
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Icon = reader.IsDBNull(3) ? null : reader.GetString(3),
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

            return Privacy;
        }


        // Add a new project using stored procedure
        public void AddPrivacy(Privacy _privacy)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddPrivacy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", _privacy.Name);
                    command.Parameters.AddWithValue("@Description", _privacy.Description);
                    command.Parameters.AddWithValue("@Icon", _privacy.Icon);
                    command.Parameters.AddWithValue("@Status", _privacy.Status);
                    command.Parameters.AddWithValue("@SEO_Title", _privacy.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", _privacy.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", _privacy.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdatePrivacy(Privacy _privacy)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("sp_UpdatePrivacy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", _privacy.Id);
                    command.Parameters.AddWithValue("@Name", _privacy.Name);
                    command.Parameters.AddWithValue("@Description", _privacy.Description);
                    command.Parameters.AddWithValue("@Icon", _privacy.Icon);
                    command.Parameters.AddWithValue("@Status", _privacy.Status);
                    command.Parameters.AddWithValue("@SEO_Title", _privacy.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", _privacy.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", _privacy.SEO_Keywords);


                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeletePrivacy(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeletePrivacy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
