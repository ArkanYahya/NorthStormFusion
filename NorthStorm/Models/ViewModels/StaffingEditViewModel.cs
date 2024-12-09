using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{
    public class StaffingEditViewModel
    {
        [Display(Name = "المعرف")]
        public int StaffingId { get; set; }
        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;
        [Required, Display(Name = " الدرجات")]
        public int StaffingCount { get; set; }

        [Range(0, 9999)]
        [Display(Name = "عدد الموظفين")]
        public int EmployeeCounted { get; set; }

        [Range(0, 9999)]
        [Display(Name = "الشاغر")]
        public int VacantStaffing { get; set; }
        [Display(Name = "الملاك الوظيفي")]
        public int? StaffingJobTitleId { get; set; }
        [Display(Name = "الوحدة الادارية")]
        public int? StaffingUnitId { get; set; }

        public List<EmployeesInfo11> Employees { get; set; } = new List<EmployeesInfo11>();
    }

    public class EmployeesInfo11
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
