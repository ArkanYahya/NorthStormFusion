using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class ResponsibleClassification     //  أنواع المسؤوليات الوظيفية
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

		[Required(ErrorMessage = "يرجى إدخال نوع المسؤولية مثال مسؤول وحدة، مسؤول شعبة، مدير قسم .....الخ"),]
		[Display(Name = "المسؤولية")]
		public string Name { get; set; }

		[Range(0, 100)]
		[Display(Name = "نسبة المخصصات")]
		public int ResponsiblityPercentage { get; set; }


		[Display(Name = "SPSS1")]
		public string ResponsiblSPSS1 { get; set; }

        [Display(Name = "SPSS2")]
        public string ResponsiblSPSS2 { get; set; }

        
		//[Required(ErrorMessage = "يرجى إدخال اسم المسؤولية باللغة الانكليزية"),
		[Display(Name = "المسؤولية باللغة الأنكليزية")]
		public string ResponsiblityInEnglish { get; set; }

		#endregion

		#region Navigstion Properties
		public ICollection<Employee> Employees { get; set; }
		#endregion
	}
}
