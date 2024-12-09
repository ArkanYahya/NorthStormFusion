using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
	public class ThankClassification         // الشكر والتقدير
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

		[Required(ErrorMessage = "يرجى إدخال وظيفة المخول بمنح الشكر والتقدير مثال ذلك مدير عام الشركة"),]
		[Display(Name = "المخول")]
		public string Name { get; set; }


        [Required(ErrorMessage = "يرجى إدخال القدم الممنوح شهر واحد أو ستة أشهر"),]
        [Range(0, 6)]
		[Display(Name = "القدم الممنوح/ شهر")]
		public int ThankSeniority { get; set; }

		


		#endregion

		#region Navigstion Properties
		#endregion
	}
}
