using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class StaffingCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
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
        public List<int> EmployeeIds { get; set; }
    }

}
