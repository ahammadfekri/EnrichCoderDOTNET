using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.SHARED;
using WCRM.MODEL.AdminPanel;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WCRM.DataAccess.AdminPanel
{
    public class User_DataAccess
    {
        SqlConnection con = null;

        // ✅ Validate User (Login)
        public bool ValidateUser(UserModel objUser)
        {
            bool result = false;
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("VALIDATE_USER_BY_USERID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", objUser.Username);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedPasswordHash = reader["PasswordHash"].ToString();
                    string storedSalt = reader["Salt"].ToString();
                    string enteredPasswordHash = HashPassword(objUser.PasswordHash, storedSalt);

                    if (enteredPasswordHash == storedPasswordHash)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Dispose();
            }
            return result;
        }

        // ✅ Create New User (Registration)
        public bool CreateUser(UserModel objUser)
        {
            bool result = false;
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("CREATE_NEW_USER", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //// Generate Salt & Hash Password
                //string salt = GenerateSalt();
                //string hashedPassword = HashPassword(objUser.PasswordHash, salt);

                cmd.Parameters.AddWithValue("@Username", objUser.Username);
                cmd.Parameters.AddWithValue("@FullName", objUser.FullName);
                cmd.Parameters.AddWithValue("@PasswordHash", objUser.PasswordHash);
                cmd.Parameters.AddWithValue("@Salt", objUser.Salt);

                con.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Dispose();
            }
            return result;
        }

        // ✅ Generate Salt
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // ✅ Hash Password using SHA-256
        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = passwordBytes.Concat(saltBytes).ToArray();

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // ✅ Get User by Username (For Login)
        public UserModel GetUserByUsername(string username)
        {
            UserModel user = null;
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("GET_USER_BY_USERNAME", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserModel
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Salt = reader["Salt"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Dispose();
            }
            return user;
        }

    }
}
