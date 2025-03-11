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
  
    public class Service_DataAccess
    {

        private readonly string _connectionString;

        public Service_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Service> GetAllServices()
        {
            var services = new List<Service>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllServices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
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

            return services;
        }

        public Service GetServiceById(long id)
        {
            Service service = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetServiceById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            service = new Service
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

            return service;
        }


        // Add a new project using stored procedure
        public void AddService(Service Service)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", Service.Name);
                    command.Parameters.AddWithValue("@Description", Service.Description);
                    command.Parameters.AddWithValue("@Icon", Service.Icon);
                    command.Parameters.AddWithValue("@Status", Service.Status);
                    command.Parameters.AddWithValue("@SEO_Title", Service.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", Service.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", Service.SEO_Keywords);


                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdateService(Service Service)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Service.Id);
                    command.Parameters.AddWithValue("@Name", Service.Name);
                    command.Parameters.AddWithValue("@Description", Service.Description);
                    command.Parameters.AddWithValue("@Icon", Service.Icon);
                    command.Parameters.AddWithValue("@Status", Service.Status);
                    command.Parameters.AddWithValue("@SEO_Title", Service.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", Service.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", Service.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeleteService(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteService", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
