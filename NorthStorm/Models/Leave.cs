using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Leave
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Required, Display(Name = "السبب")]
        public string LeaveReason { get; set; }

        [Range(0,30)]
        [Required, Display(Name = "يوم")]
        public int LeaveInDays { get; set; }

        [Range(0, 11)]
        [Required, Display(Name = "شهر")]
        public int LeaveInMonthes { get; set; }

        [Range(0, 5)]
        [Required, Display(Name = "سنة")]
        public int LeaveInYears { get; set; }

        [Display(Name = "لانفكاك")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OnLeaveDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date), Display(Name = "المباشرة المفترض")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime SupposedEnrollDate { get; set; } = DateTime.Now;


        [Display(Name = "المباشرة الفعلي")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollDate { get; set; }


        //[Display(Name = "المباشرة الفعلي")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public DateTime? EnrollDate { get; set; } = DateTime.Now;

        #endregion

        #region Foreign Key
        [Display(Name = "الاجازة")]
        public int? LeaveClassificationId { get; set; }
        #endregion

        #region Navigation Properties
        public LeaveClassification LeaveClassification { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
