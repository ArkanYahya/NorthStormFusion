using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NorthStorm.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace NorthStorm.Models.Validations
{
    public class EmployeeIdValidation : ValidationAttribute
    {
        public string GetErrorMessage_Mod() =>
            $"رقم الموظف الذي ادخلته غير صحيح.";
        public string GetErrorMessage_Duplicate() =>
            $"رقم الموظف مستخدم بالفعل.";

        private ValidationContext _validationContext;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            _validationContext = validationContext;
            var entity = (Employee)validationContext.ObjectInstance;

            if (value != null)
            {
                if (!checkEmployeeId_Mod(int.Parse(value.ToString())))
                {
                    return new ValidationResult(GetErrorMessage_Mod());
                }
                if (entity.IsCreateAction)
                {
                    if (checkEmployeeId_Duplicate(int.Parse(value.ToString())))
                    {
                        return new ValidationResult(GetErrorMessage_Duplicate());
                    }

                }
            }
            return ValidationResult.Success;
        }

        private bool checkEmployeeId_Mod(int id)
        {
            bool chk = false;

            if (id.ToString().Length == 6)
            {
                int x = int.Parse(id.ToString().Substring(0, 5));
                x = x % 11;
                if (x == 10) { x = 0; }
                chk = x == int.Parse(id.ToString().Substring(5, 1));
                return chk;
            }
            return chk;
        }

        private bool checkEmployeeId_Duplicate(int id)
        {
            var _context = (NorthStormContext)_validationContext
             .GetService(typeof(NorthStormContext));
            return _context.Employees.Any(c => c.Id == id);
        }
    }

}