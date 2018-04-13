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
using WebApp_Controller.ViewModels;

namespace WebApp_Controller.Controllers
{
    public class BulkUploadController : AsyncController
    {
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        [HandleError]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>(
                () => GetEmployees(model));

            int t2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
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