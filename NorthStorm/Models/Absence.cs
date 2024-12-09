using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Absence
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
        public string AbsenceReason { get; set; }

        [Range(0,30)]
        [Required, Display(Name = "يوم")]
        public int AbsenceInDays { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخ الغياب")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OnAbsenceDate { get; set; } = DateTime.Now;

       
        [Required, DataType(DataType.Date), Display(Name = "تاريخ المباشرة")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollDate { get; set; } = DateTime.Now;


        #endregion
        #region Foreign Key
        //[Display(Name = "تصنيف الغياب")]
        //public int? AbsenceClassificationId { get; set; }
        #endregion

        #region Navigation Properties
        //public AbsenceClassification AbsenceClassification { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
