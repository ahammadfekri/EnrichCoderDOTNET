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
        public bool ValidateUser(UserModel objUser)
        {

            bool result = false;
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("VALIDATE_USER_BY_USERID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", objUser.Username);
                cmd.Parameters.AddWithValue("@Password", objUser.PasswordHash);
                con.Open();
                 int count = (int)cmd.ExecuteScalar();
                if(count>0)
                {
                    result = true;  
                }
            }
            catch
            {
                
            }
            finally
            {
                con.Dispose();
            }
            return result;
        }

        
    }
}
