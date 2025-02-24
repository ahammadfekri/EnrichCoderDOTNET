using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCRM.DataAccess.SHARED;
using WCRM.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WCRM.DataAccess.HRMS
{
    public class Employees_DataAccess
    {
        SqlConnection con = null;
        public string Add(Employee objEmp)
        {

            string result = "";
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("HRMS_EMPLOYEE_INSERT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpCODE", objEmp.Code);
                cmd.Parameters.AddWithValue("@EmpName", objEmp.Name);
                cmd.Parameters.AddWithValue("@EmpAddres", objEmp.Address);
                con.Open();
                result = cmd.ExecuteNonQuery().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Dispose();
            }

        }

        public string Edit(Employee objEmp)
        {

            string result = "";
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("HRMS_EMPLOYEE_UPDATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objEmp.ID);
                cmd.Parameters.AddWithValue("@EmpCODE", objEmp.Code);
                cmd.Parameters.AddWithValue("@EmpName", objEmp.Name);
                cmd.Parameters.AddWithValue("@EmpAddres", objEmp.Address);
                con.Open();
                result = cmd.ExecuteNonQuery().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Dispose();
            }
        }

        public List<Employee> GetAll()
        {
            List<Employee> lstEmployee = new List<Employee>();
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("HRMS_EMPLOYEE_GET_ALL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt64(rdr["ID"]);
                    employee.Code = rdr["CODE"].ToString();
                    employee.Name = rdr["Name"].ToString();
                    employee.Address = rdr["Address"].ToString();

                    lstEmployee.Add(employee);
                }                
            }
            catch
            {
                
            }
            finally
            {
                con.Dispose();
            }
            return lstEmployee;
        }

        public Employee GetEmployeeByID(Int64 Id)
        {
            Employee employee = new Employee();
            try
            {
                con = new SqlConnection(DBConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("HRMS_EMPLOYEE_GET_BY_EMPID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", Id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {                   
                    employee.ID = Convert.ToInt64(rdr["ID"]);
                    employee.Code = rdr["CODE"].ToString();
                    employee.Name = rdr["Name"].ToString();
                    employee.Address = rdr["Address"].ToString();
                }
            }
            catch
            {

            }
            finally
            {
                con.Dispose();
            }
            return employee;
        }
    }
}
