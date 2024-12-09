using NorthStorm.Models.Assistants;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models
{
    public class Promotion
    {
        #region Model Properties
        public int Id { get; set; }

      //  [Required, Display(Name = "الوجبة")]

        [ Display(Name = "الوجبة")]
        public string BatchNo { get; set; }

       [Required, Display(Name = "السنة")]
        public int PromotionMinutesYear { get; set; }

      //  [Required, Display(Name = "الموضوع")]

        [ Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Display(Name = "رقم الامر")]
        public string OrderNo { get; set; }

        [ Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [ DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;
        #endregion

        #region Navigation Properties
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
#endregion
    }
}
