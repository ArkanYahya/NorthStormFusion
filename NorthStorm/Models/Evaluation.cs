using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Evaluation
    {
       #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
      //  [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Range(0,100)]
        [Required, Display(Name = "درجة التقييم")]
        public int EvaluationInNumber { get; set; }

        //[Display(Name = "تاريخه")]
        //[DisplayFormat(DataFormatString = "{yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //public DateOnly EvaluationDate { get => EvaluationDate; set => EvaluationDate = value; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخه")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EvaluationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "يرجى ادخال الدرجة المالية"),
        Display(Name = "الدرجة المالية")]
        public int SalaryGradeNumber { get; set; }

        [Required(ErrorMessage = "يرجى ادخال المرحلة المالية"),
         Display(Name = "المرحلة رقما")]
         public int StageNumber { get; set; }


        // يتم عرض الراتب الاسمي الحالي فورا


        [Required(ErrorMessage = "يرجى ادخال الدرجة الوظيفية"),
       Display(Name = "الدرجة رقما")]
        public int JobGradeNumber { get; set; }

        // يتم احتساب عدد الشكر والتقدير الحاليين

        // يتم تحديد تاريخ العلاوة القادمة

        #endregion

        #region Foreign Key
        [Display(Name = "التقييم")]
        public int? EvaluationClassificationId { get; set; }
        #endregion

        #region Navigation Properties
        public EvaluationClassification EvaluationClassification { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();

        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
