using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.SHARED;

namespace WCRM.SERVICESS.SHARED
{
    public static class DBConnectionServices
    {
             
        public static void DatabaseConn(string connectionString)
        {
            DBConnection.ConnectionString(connectionString);
        }
    }
}
