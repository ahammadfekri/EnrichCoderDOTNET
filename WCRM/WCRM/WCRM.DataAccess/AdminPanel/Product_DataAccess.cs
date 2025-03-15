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
  
    public class Product_DataAccess
    {

        private readonly string _connectionString;

        public Product_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Product> GetAllProducts()
        {
            var Industries = new List<Product>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Industries.Add(new Product
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

        public Product GetProductById(long id)
        {
            Product Product = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Product = new Product
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

            return Product;
        }


        // Add a new project using stored procedure
        public void AddProduct(Product Product)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", Product.Name);
                    command.Parameters.AddWithValue("@Description", Product.Description);
                    command.Parameters.AddWithValue("@Icon", Product.Icon);
                    command.Parameters.AddWithValue("@Status", Product.Status);
                    command.Parameters.AddWithValue("@SEO_Title", Product.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", Product.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", Product.SEO_Keywords);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdateProduct(Product Product)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Product.Id);
                    command.Parameters.AddWithValue("@Name", Product.Name);
                    command.Parameters.AddWithValue("@Description", Product.Description);
                    command.Parameters.AddWithValue("@Icon", Product.Icon);
                    command.Parameters.AddWithValue("@Status", Product.Status);
                    command.Parameters.AddWithValue("@SEO_Title", Product.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", Product.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", Product.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeleteProduct(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
