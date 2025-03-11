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
  
    public class Industry_DataAccess
    {

        private readonly string _connectionString;

        public Industry_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Industry> GetAllIndustries()
        {
            var Industries = new List<Industry>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllIndustries", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Industries.Add(new Industry
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

            return Industries;
        }

        public Industry GetIndustryById(long id)
        {
            Industry Industry = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetIndustryById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Industry = new Industry
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

            return Industry;
        }


        // Add a new project using stored procedure
        public void AddIndustry(Industry industry)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddIndustry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", industry.Name);
                    command.Parameters.AddWithValue("@Description", industry.Description);
                    command.Parameters.AddWithValue("@Icon", industry.Icon);
                    command.Parameters.AddWithValue("@Status", industry.Status);
                    command.Parameters.AddWithValue("@SEO_Title", industry.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", industry.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", industry.SEO_Keywords);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdateIndustry(Industry industry)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateIndustry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", industry.Id);
                    command.Parameters.AddWithValue("@Name", industry.Name);
                    command.Parameters.AddWithValue("@Description", industry.Description);
                    command.Parameters.AddWithValue("@Icon", industry.Icon);
                    command.Parameters.AddWithValue("@Status", industry.Status);
                    command.Parameters.AddWithValue("@SEO_Title", industry.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", industry.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", industry.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeleteIndustry(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteIndustry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
