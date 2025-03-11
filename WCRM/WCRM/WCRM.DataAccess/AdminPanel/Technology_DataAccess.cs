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
  
    public class Technology_DataAccess
    {

        private readonly string _connectionString;

        public Technology_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Technology> GetAllTechnologies()
        {
            var technologies = new List<Technology>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllTechnology", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            technologies.Add(new Technology
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Icon = reader.GetString(3),
                                CreatedAt = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                UpdatedAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                Status = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return technologies;
        }

        public Technology GetTechnologyById(long id)
        {
            Technology technology = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetTechnologyById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            technology = new Technology
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Icon = reader.GetString(3),
                                CreatedAt = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                UpdatedAt = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                Status = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return technology;
        }


        // Add a new project using stored procedure
        public void AddTechnology(Technology technology)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddTechnology", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", technology.Name);
                    command.Parameters.AddWithValue("@Description", technology.Description);
                    command.Parameters.AddWithValue("@Icon", technology.Icon);
                    command.Parameters.AddWithValue("@CreatedAt", technology.CreatedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedAt", technology.UpdatedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", technology.Status);

                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdateTechnology(Technology technology)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateTechnology", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", technology.Id);
                    command.Parameters.AddWithValue("@Name", technology.Name);
                    command.Parameters.AddWithValue("@Description", technology.Description);
                    command.Parameters.AddWithValue("@Icon", technology.Icon);
                    command.Parameters.AddWithValue("@UpdatedAt", technology.UpdatedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", technology.Status);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeleteTechnology(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteTechnology", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
