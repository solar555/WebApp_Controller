using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp_Controller.Filters;
using WebApp_Controller.Models;
using WebApp_Controller.ViewModels.SPA;

namespace WebApp_Controller.Areas.SPA.Controllers
{
    public class SpaBulkUploadController : Controller
    {
        [AdminFilter]
        // GET: SPA/SpaBulkUpload
        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>
                (() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            EmployeeListViewModel vm = new EmployeeListViewModel();
            vm.Employees = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = item.FirstName + " " + item.LastName;
                evm.Salary = item.Salary.Value.ToString("C");
                if (item.Salary > 15000)
                {
                    evm.SalaryColor = "yellow";
                }
                else
                {
                    evm.SalaryColor = "green";
                }
                vm.Employees.Add(evm);
            }

            return Json(vm);
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvReader = new StreamReader(model.fileUpload.InputStream);
            csvReader.ReadLine();
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(',');
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }
    }
}