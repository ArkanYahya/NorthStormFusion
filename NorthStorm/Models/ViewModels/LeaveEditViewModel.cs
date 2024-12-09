using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class LeaveEditViewModel
    {
        public int LeaveId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
        
        [Required, Display(Name = "السبب")]
        public string LeaveReason { get; set; }

        [Range(0, 30)]
        //[Required, Display(Name = "مدة الاجازة/ يوم")] 
        [Required, Display(Name = "يوم")]
        public int LeaveInDays { get; set; }

        [Range(0, 11)]
      //  [Required, Display(Name = "مدة الاجازة/شهر")]
        [Required, Display(Name = "شهر")]
        public int LeaveInMonthes { get; set; }

        [Range(0, 5)]
        //[Required, Display(Name = "مدة الاجازة/سنة")]
        [Required, Display(Name = "سنة")]
        public int LeaveInYears { get; set; }

        [Display(Name = "ت الانفكاك")]
        public DateTime OnLeaveDate { get; set; } = DateTime.Now;

        [Display(Name = "المباشرة المفترض")]
        public DateTime SupposedEnrollDate { get; set; } = DateTime.Now;

               
        [Display(Name = "المباشرةالفعلي")]
        public DateTime EnrollDate { get; set; }


        [Display(Name = "الاجازة")]
        public int? LeaveClassificationId { get; set; }
        public LeaveClassification LeaveClassification { get; set; }
        public List<EmployeesInfo3> Employees { get; set; } = new List<EmployeesInfo3>();
    }

    public class EmployeesInfo3
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
