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
  
    public class Contact_DataAccess
    {

        private readonly string _connectionString;

        public Contact_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Contact> GetAllContacts()
        {
            var contacts = new List<Contact>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Contact
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

            return contacts;
        }

        public Contact GetContactById(long id)
        {
            Contact Contact = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetContactById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Contact = new Contact
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

            return Contact;
        }


        // Add a new project using stored procedure
        public void AddContact(Contact contact)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("AddContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", contact.Name);
                    command.Parameters.AddWithValue("@Description", contact.Description);
                    command.Parameters.AddWithValue("@Icon", contact.Icon);
                    command.Parameters.AddWithValue("@Status", contact.Status);
                    command.Parameters.AddWithValue("@SEO_Title", contact.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", contact.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", contact.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }


        // Update project
        public void UpdateContact(Contact contact)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", contact.Id);
                    command.Parameters.AddWithValue("@Name", contact.Name);
                    command.Parameters.AddWithValue("@Description", contact.Description);
                    command.Parameters.AddWithValue("@Icon", contact.Icon);
                    command.Parameters.AddWithValue("@Status", contact.Status);
                    command.Parameters.AddWithValue("@SEO_Title", contact.SEO_Title);
                    command.Parameters.AddWithValue("@SEO_Description", contact.SEO_Description);
                    command.Parameters.AddWithValue("@SEO_Keywords", contact.SEO_Keywords);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a project using stored procedure
        public void DeleteContact(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
