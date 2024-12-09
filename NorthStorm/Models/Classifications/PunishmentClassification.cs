using NorthStorm.Models.Assistants;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class PunishmentClassification       //  أنواع العقوبات الوظيفية
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

		[Required(ErrorMessage = "يرجى إدخال العقوبة : مثال لفت نظر، انذار، توبيخ، .....الخ"),]
		[Display(Name = "العقوبة")]
		public string Name { get; set; }

		[Range(-50, 50)]
		[Required(ErrorMessage = "العقوبات بتاثير تبدا من لفت النظر و شدتها 1 و تنتهي بالعزل وشدتها 13"),]
		[Display(Name = "شدة العقوبة")]
		public int PunishmentSeverity { get; set; }


		[Display(Name = "اثر العقوبة كتابة")]
		public string PunishmentWriting { get; set; }

		[Range(-36, 36)]
		[Required(ErrorMessage = "يرجى إدخال تأثير العقوبة بعدد الاشهر التي تأخر العلاوة او الترفيع"),
		Display(Name = "تأخير العلاوة/ شهرا")]
		public int PunishmentEffect { get; set; }

        #endregion

        #region Navigation Properties
        public ICollection<Punishment> Punishments { get; set; }
        #endregion







    }
}
