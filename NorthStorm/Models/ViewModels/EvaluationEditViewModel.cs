using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class EvaluationEditViewModel
    {
        public int EvaluationId { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
    //    [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;



        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Range(0, 100)]
        [Required, Display(Name = "درجة التقييم")]
        public int EvaluationInNumber { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخه")]
      //  [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public DateTime EvaluationDate { get; set; } = DateTime.Now;

      
        [Required(ErrorMessage = "يرجى ادخال الدرجة المالية"),
        Display(Name = "الدرجة المالية")]
        public int SalaryGradeNumber { get; set; }

        [Required(ErrorMessage = "يرجى ادخال المرحلة المالية"),
         Display(Name = "المرحلة رقما")]
        public int StageNumber { get; set; }

        // يتم عرض الراتب الاسمي الحالي فورا

        [Required(ErrorMessage = "يرجى ادخال الدرجة الوظيفية"),
        Display(Name = "الدرجةالوظيقية")]
        public int JobGradeNumber { get; set; }

        //public List<int> EmployeeIds { get; set; }

        [Display(Name = "التقييم")]
        public int? EvaluationClassificationId { get; set; }
        public EvaluationClassification EvaluationClassification { get; set; }
        public List<EmployeesInfo4> Employees { get; set; } = new List<EmployeesInfo4>();
    }

    public class EmployeesInfo4
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
