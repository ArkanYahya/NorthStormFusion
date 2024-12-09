using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class MilitaryClassification           // تصنيفات الخدمة العسكرية
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "الخدمة العسكرية")]
        public string Name { get; set; }

       
        #endregion

        #region Navigstion Properties
        public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
