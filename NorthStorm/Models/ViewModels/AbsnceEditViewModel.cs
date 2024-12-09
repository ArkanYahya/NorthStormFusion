using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class AbsenceEditViewModel
    {
        public int AbsenceId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
        
        [Required, Display(Name = "السبب")]
        public string AbsenceReason { get; set; }

        [Required, DataType(DataType.Date),Display(Name = "تاريخ الغياب")]
        public DateTime OnAbsenceDate { get; set; } = DateTime.Now;
               
        [Required, DataType(DataType.Date), Display(Name = "تاريخ المباشرة")]
        public DateTime EnrollDate { get; set; }

        //  من المفترض حسابة اوتماتيكيا بعد وضع تاريخ المباشرة
        [Required, Display(Name = "مدة الغياب/يوم")]
        public int AbsenceInDays { get; set; }


        //[Display(Name = "الاجازة")]
        //public int? AbsenceClassificationId { get; set; }
        //public AbsenceClassification AbsenceClassification { get; set; }
        public List<EmployeesInfo12> Employees { get; set; } = new List<EmployeesInfo12>();
    }

    public class EmployeesInfo12
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
