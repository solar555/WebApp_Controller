﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_Controller.Models;
using WebApp_Controller.ViewModels.SPA;
using OldViewModel = WebApp_Controller.ViewModels;
using WebApp_Controller.Filters;

namespace WebApp_Controller.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "StepByStepSchools";
            v.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", v);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            return View("EmployeeList", employeeListViewModel);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", v);
        }

        [AdminFilter]
        public ActionResult SaveEmployee(Employee emp)
        {
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            empBal.SaveEmployee(emp);

            EmployeeViewModel empViewModel = new EmployeeViewModel();
            empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
            empViewModel.Salary = emp.Salary.Value.ToString("C");
            if (emp.Salary > 15000)
            {
                empViewModel.SalaryColor = "yellow";
            }
            else
            {
                empViewModel.SalaryColor = "green";
            }
            
            return Json(empViewModel);
        }
    }
}