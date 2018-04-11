using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp_Controller.CustomerValidation
{
    public class FirstNameValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please Provide First Name");
            }
            else
            {
                if (!value.ToString().Contains("@"))
                {
                    return new ValidationResult("First Name should contain @");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class SalaryValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult($"The value '{value}' is not valid for Salary.");

            int salary = 0;
            bool isOK = int.TryParse(value.ToString(), out salary);
            if (!isOK)
                return new ValidationResult($"The value '{value.ToString()}' is not valid for Salary.");

            if (salary < 5000 || salary > 50000)
            {
                return new ValidationResult("Put a proper Salary value between 5000 and 50000.");
            }
            return ValidationResult.Success;
        }
    }
}