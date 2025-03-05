using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WCRM.DataAccess.SHARED;
using WCRM.MODEL.AdminPanel;

namespace WCRM.DataAccess.AdminPanel
{
    public class HeroSection_DataAccess
    {
        private readonly string _connectionString;

        public HeroSection_DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all hero sections
        public List<HeroSectionModel> GetAllHeroSections()
        {
            var heroSections = new List<HeroSectionModel>();

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllHeroSections", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            heroSections.Add(new HeroSectionModel
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? "" : reader.GetString(reader.GetOrdinal("Title")),
                                Subtitle = reader.IsDBNull(reader.GetOrdinal("Subtitle")) ? "" : reader.GetString(reader.GetOrdinal("Subtitle")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                                BackgroundImage = reader.IsDBNull(reader.GetOrdinal("BackgroundImage")) ? null : reader.GetString(reader.GetOrdinal("BackgroundImage")),
                                ButtonText = reader.IsDBNull(reader.GetOrdinal("ButtonText")) ? "" : reader.GetString(reader.GetOrdinal("ButtonText")),
                                ButtonLink = reader.IsDBNull(reader.GetOrdinal("ButtonLink")) ? "" : reader.GetString(reader.GetOrdinal("ButtonLink")),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? "" : reader.GetString(reader.GetOrdinal("Status")),
                                SEO_Title = reader.IsDBNull(reader.GetOrdinal("SEO_Title")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Title")),
                                SEO_Description = reader.IsDBNull(reader.GetOrdinal("SEO_Description")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Description")),
                                SEO_Keywords = reader.IsDBNull(reader.GetOrdinal("SEO_Keywords")) ? "" : reader.GetString(reader.GetOrdinal("SEO_Keywords"))
                            });
                        }
                    }
                }
            }

            return heroSections;
        }


        // Get hero section by ID
        public HeroSectionModel GetHeroSectionById(int id)
        {
            HeroSectionModel heroSection = null;

            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetHeroSectionById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            heroSection = new HeroSectionModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Subtitle = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                BackgroundImage = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ButtonText = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                ButtonLink = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Status = reader.GetString(7),
                                SEO_Title = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                SEO_Description = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                SEO_Keywords = reader.IsDBNull(10) ? "" : reader.GetString(10)
                            };
                        }
                    }
                }
            }

            return heroSection;
        }

        // Add a new hero section
        public void AddHeroSection(HeroSectionModel heroSection)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("InsertHeroSection", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", heroSection.Title);
                    command.Parameters.AddWithValue("@Subtitle", heroSection.Subtitle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", heroSection.Description);
                    command.Parameters.AddWithValue("@BackgroundImage", heroSection.BackgroundImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ButtonText", heroSection.ButtonText ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ButtonLink", heroSection.ButtonLink ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", heroSection.Status);

                    // SEO Fields
                    command.Parameters.AddWithValue("@SEO_Title", heroSection.SEO_Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Description", heroSection.SEO_Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Keywords", heroSection.SEO_Keywords ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Update an existing hero section
        public void UpdateHeroSection(HeroSectionModel heroSection)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateHeroSection", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", heroSection.Id);
                    command.Parameters.AddWithValue("@Title", heroSection.Title);
                    command.Parameters.AddWithValue("@Subtitle", heroSection.Subtitle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", heroSection.Description);
                    command.Parameters.AddWithValue("@BackgroundImage", heroSection.BackgroundImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ButtonText", heroSection.ButtonText ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ButtonLink", heroSection.ButtonLink ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", heroSection.Status);

                    // SEO Fields
                    command.Parameters.AddWithValue("@SEO_Title", heroSection.SEO_Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Description", heroSection.SEO_Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SEO_Keywords", heroSection.SEO_Keywords ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a hero section
        public void DeleteHeroSection(int id)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteHeroSection", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
