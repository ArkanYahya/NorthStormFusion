using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Staffing
    {
      
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = " الدرجات")]
        public int StaffingCount { get; set; }


        //    من المفترض تحتسب تلقائيا و تخزن
        [Range(0, 9999)]
        [Display(Name = "عدد الموظفين")]
        public int EmployeeCounted { get; set; }


        //   من المفترض تحتسب تلقائيا مع الخزن
        [Range(0, 9999)]
        [Display(Name = "الشاغر")]
        public int VacantStaffing { get; set; }

        
        #endregion
        #region Foreign Key
        [Display(Name = "الملاك الوظيفي")]
        public int? StaffingJobTitleId { get; set; }
        [Display(Name = "الوحدة الادارية")]
        public int? StaffingUnitId { get; set; }
        #endregion

        #region Navigation Properties
        public JobTitle StaffingJobTitle { get; set; }
        public Level StaffingUnit { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion


    }
}
