using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.HRMS;
using WCRM.Models;

namespace WCRM.SERVICESS.HRMS
{
    public class EmployeeServices
    {
        public EmployeeServices() { }
        Employees_DataAccess objAccess;
        public string Add(Employee obj)
        {
            string returnResult = "99";
            objAccess = new Employees_DataAccess();
            returnResult = objAccess.Add(obj);
            return returnResult;
        }
        public string Edit(Employee Emp)
        {
            string returnResult = "99";
            objAccess = new Employees_DataAccess();
            returnResult = objAccess.Edit(Emp);
            return returnResult;
        }
        public List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>(); 
            objAccess = new Employees_DataAccess();
            list = objAccess.GetAll();
            return list;
        }
        public Employee GetEmployeeByID(Int64 Id)
        {
            Employee objGetEmployee = new Employee();
            objAccess = new Employees_DataAccess();
            objGetEmployee = objAccess.GetEmployeeByID(Id);
            return objGetEmployee;
        }
    }
}
