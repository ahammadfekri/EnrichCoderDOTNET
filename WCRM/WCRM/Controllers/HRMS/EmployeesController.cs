using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCRM.DataAccess.HRMS;
using WCRM.Models;
using WCRM.SERVICESS.HRMS;

namespace WCRM.Controllers.HRMS
{
    public class EmployeesController : Controller
    {
        // GET: EmployeesController
        Employees_DataAccess employees_DataAccess;
        EmployeeServices employeeServices;
       public EmployeesController()
        {
            employees_DataAccess = new Employees_DataAccess(); 
        }
        public ActionResult Index()
        {
            List<Employee> emplist = new List<Employee>();
            employeeServices = new EmployeeServices();
            emplist = employeeServices.GetAll();
            return View(emplist);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee objEmp)
        {
            try
            {
                // TODO: Add insert logic here  
                employeeServices = new EmployeeServices();
                employeeServices.Add(objEmp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(Int64 id)
        {
            Employee objEmp = new Employee();
            employeeServices = new EmployeeServices();
            objEmp = employeeServices.GetEmployeeByID(9);
            return View(objEmp);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee objEmp)
        {
            try
            {
                employeeServices = new EmployeeServices();
                employeeServices.Edit(objEmp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
