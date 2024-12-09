using System.ComponentModel.DataAnnotations;

    namespace NorthStorm.Models.Classifications        // الفولدر الجديد كلاسيفيكشن داخل المودلز
{
    public class CareerClassification         // المهنة والدور الوظيفي
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "المهنة/الدورالوظيفي")]
        public string Name { get; set; }

       
        #endregion

        #region Navigstion Properties
        public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
