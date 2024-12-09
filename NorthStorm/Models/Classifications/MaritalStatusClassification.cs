using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class MaritalStatusClassification        //  الحالة الاجتماعية ... اعزب . متزوج ....الخ
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

		[Required(ErrorMessage = "يرجى إدخال الحالة الاجتماعية مثال اعزب، متزوج .....الخ"),]
		[Display(Name = "الحالة الاجنماعية")]
		public string Name { get; set; }

		[Range(0, 100000)]
		[Display(Name = "مبلغ المخصصات")]
		public int MaritalStatusAllowance { get; set; }


		[Display(Name = "SPSS")]
		public string MaritalStatusSPSS { get; set; }

      
		#endregion

		#region Navigstion Properties
		public ICollection<Employee> Employees { get; set; }
		#endregion
	}
}
