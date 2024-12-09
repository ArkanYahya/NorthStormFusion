using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Retirement
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }
     
        [DataType(DataType.Date), Display(Name = "إعتبارا من")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DisengagementDate { get; set; } = DateTime.Now;
        #endregion

        #region Foreign Key
        [Display(Name = "التقاعد")]
        public int? DismissClassificationId { get; set; }
        public int? StatusId { get; set; }
        #endregion

        #region Navigation Properties
        public DismissClassification DismissClassification { get; set; }
        public Status Status { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
