using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApp_Controller.CustomerValidation;

namespace WebApp_Controller.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [FirstNameValidation]
        public string FirstName { get; set; }

        [StringLength(5, ErrorMessage ="Last Name length should not be greater than 5")]
        public string LastName { get; set; }

        [SalaryValidation]
        public int? Salary { get; set; }
    }
}