using NorthStorm.Models.Assistants;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class DismissClassification       // تصنيفات مغادرة ملاك الشركة تقاعد، استقالة، ترك العمل... الخ
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "تصنيف الطرح")]
        public string Name { get; set; }

       
        [Display(Name = "SPSS")]
        public string DismissSPSSCode { get; set; }
        #endregion

        #region Foreign Keys

        public int? StatusId { get; set; }          // Foreign Key

        #endregion

        #region Navigstion Properties

        [Display(Name = "الطرح المؤثر")]
        public Status status { get; set; }
        public ICollection<Employee> Employees { get; set; }
       // public ICollection<Dismiss> Dismisses { get; set; }

        #endregion
    }
}
