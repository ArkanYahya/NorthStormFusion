using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models
{
    public class EmployeeJobTitle
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }

        public string ReferenceNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReferenceDate { get; set; }

        [Display(Name = "تاريخ الإستحقاق")]
        public DateTime JobTitleAssignedDate { get; set; } // Track when the job title was assigned
    }
}