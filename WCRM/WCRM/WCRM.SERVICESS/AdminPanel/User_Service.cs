using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.AdminPanel;
using WCRM.MODEL.AdminPanel;

namespace WCRM.SERVICESS.AdminPanel
{
    public class User_Service
    {
        public User_Service() { }
        User_DataAccess objUserDAccess;
        public bool ValidateUser(UserModel objUser)
        {
            bool rusult = false;
            objUserDAccess = new User_DataAccess();
            rusult = objUserDAccess.ValidateUser(objUser);
            return rusult;
        }
    }
}
