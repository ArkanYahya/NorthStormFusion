using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class AbsenceCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Required, Display(Name = "السبب")]
        public string AbsenceReason { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخ الغياب")]
        public DateTime OnAbsenceDate { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Date), Display(Name = "تاريخ المباشرة")]
        public DateTime EnrollDate { get; set; } = DateTime.Now;

        //  من المفترض حسابة اوتماتيكيا بعد وضع تاريخ المباشرة
        [Required, Display(Name = "مدة الغياب/يوم")]
        public int AbsenceInDays { get; set; }


        //public AbsenceClassification AbsenceClassification { get; set; }
        public List<int> EmployeeIds { get; set; }
    }

}
