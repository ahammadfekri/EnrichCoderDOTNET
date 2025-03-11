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
  
    public class Project_DataAccess
    {

        private readonly string _connectionString;

        public Project_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all projects
        //public List<ProjectModel> GetAllProjects()
        //{
        //    var projects = new List<ProjectModel>();

        //    using (var connection = DBConnection.GetConnection())
        //    {
        //        connection.Open();
        //        using (var command = new SqlCommand("GetAllProjects", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    projects.Add(new ProjectModel
        //                    {
        //                        Id = reader.GetInt32(0),
        //                        Title = reader.GetString(1),
        //                        Description = reader.GetString(2),
        //                        ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
        //                        SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
        //                        SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
        //                        SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords")),
        //                        Status = reader.GetString(4), // Read Status from DB
        //                        CreatedAt = reader.GetDateTime(5),
        //                        UpdatedAt = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return projects;
        //}

        public List<ProjectModel> GetAllProjects()
        {
            var projects = new List<ProjectModel>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllProjects", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(new ProjectModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),  
                                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Status = reader.GetString(4),
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

            return projects;
        }

        // Get project by ID
        public ProjectModel GetProjectById(int id)
        {
            ProjectModel project = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetProjectById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            project = new ProjectModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Status = reader.GetString(4),
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

            return project;
        }

        // Add a new project using stored procedure
        public void AddProject(ProjectModel project)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddProject", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("@Title", project.Title);
                    command.Parameters.AddWithValue("@Description", project.Description);
                    command.Parameters.AddWithValue("@ImageUrl", project.ImageUrl ?? (object)DBNull.Value);

                    // SEO Fields (Fixing typo)
                    command.Parameters.AddWithValue("@SEO_Title", project.SEO_Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Description", project.SEO_Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Keywords", project.SEO_Keywords ?? (object)DBNull.Value);  // Corrected spelling
                    command.Parameters.AddWithValue("@Status", project.Status);


                    command.ExecuteNonQuery();
                }
            }
        }

        // Update project
        public void UpdateProject(ProjectModel project)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateProject", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", project.Id);
                    command.Parameters.AddWithValue("@Title", project.Title);
                    command.Parameters.AddWithValue("@Description", project.Description);
                    command.Parameters.AddWithValue("@ImageUrl", project.ImageUrl ?? (object)DBNull.Value);

                    // SEO Fields (Fixing typo)
                    command.Parameters.AddWithValue("@SEO_Title", project.SEO_Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Description", project.SEO_Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Keywords", project.SEO_Keywords ?? (object)DBNull.Value);  // Corrected spelling
                    command.Parameters.AddWithValue("@Status", project.Status);

                    command.ExecuteNonQuery();
                }
            }
        }
        // Delete a project using stored procedure
        public void DeleteProject(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteProject", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
