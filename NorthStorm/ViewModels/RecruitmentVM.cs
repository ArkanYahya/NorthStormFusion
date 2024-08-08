using NorthStorm.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.ViewModels
{
    public class RecruitmentVM
    {
        // ***********************           Recruitment 
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now.Date;

        [Required, Display(Name = " الموضوع / رقم الأمر الإداري")]
        public string Subject { get; set; }

        // ***********************           Employees
        public Employee Employee { get; set; } = new Employee();

        // ***********************           JobTitle
        [Display(Name = "العنوان الوظيفي")]
        public int JobTitleId { get; set; }
        //public JobTitle JobTitle { get; set; } = new JobTitle();

        //[Display(Name = "تاريخ الإستحقاق")]
        //public DateTime JobTitleAssignedDate { get; set; } 
    }
}
