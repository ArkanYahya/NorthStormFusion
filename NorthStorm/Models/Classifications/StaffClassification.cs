using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class StaffClassification           // تصنيفات نوع الملاك في الشركة عقد،وقتي، مثبت... الخ
    {
        #region Model Properties


        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "الملاك")]
        public string Name { get; set; }

		[Required]
		[Display(Name = " نوع الملاك")]
		public string StaffPeriod { get; set; }
        #endregion

        #region Navigstion Properties
        public ICollection<Recruitment> Recruitment { get; set; }
        public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
