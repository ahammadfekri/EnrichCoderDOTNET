using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRM.DataAccess.SHARED
{
    public static class DBConnection
    {
        
        private static string DataAccessDBConn = string.Empty;
        public static void ConnectionString(string DBConn)
        {
            DataAccessDBConn = DBConn;
        }
        public static string GetConnectionString()
        {
             return DataAccessDBConn;
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DBConnection.GetConnectionString());
        }
    }
}
