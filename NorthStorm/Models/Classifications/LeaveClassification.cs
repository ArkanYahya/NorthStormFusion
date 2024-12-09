using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models.Classifications
{
    public class LeaveClassification           // تصنيفات الاجازات
    {
        #region Model Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "ادخال نوع الاجازة مثل اعتيادية، مرضية، دراسية ... الخ"),]
        [Display(Name = "نوع الاجازة")]
        public string Name { get; set; }


        [Display(Name = "مدة الاجازة باليوم")]
        public int LeaveIndays { get; set; }


        [Required(ErrorMessage = "يرجى تأشير المربع في حالة احتسابها خدمة وظيفية"),]
        [Display(Name = "تحتسب كخدمة")]
        public bool ServiceCharged { get; set; }

        [Required(ErrorMessage = "يرجى تأشير المربع في حالة احتسابها للترقية"),]
        [Display(Name = "تحتسب للترقية")]
        public bool PromotionCharged { get; set; }


        [Required(ErrorMessage = "يرجى تأشير المربع في حالة كونها خارج قوة العمل"),]
        [Display(Name = "خارج قوة العمل")]
        public bool OutSideLaborForce { get; set; }

        [Range (0,1)]
        [Column(TypeName = "decimal(3, 1)")]
        [Required(ErrorMessage = "يرجى إدخال نوع استحقاق الراتب 1=تام او 0,5=نصف راتب او 0=بدون راتب"),]
        [Display(Name = "احتساب الراتب")]
        public decimal SalaryCharged { get; set; }


        [Display(Name = "ملاحظات")]
        public string OtherNotes { get; set; }
        #endregion

        #region Foreign Keys
         public int? GenderId { get; set; }          // Foreign Key
        #endregion

        #region Navigstion Properties
        [Display(Name = "الجنس غير المشمول")]
        public Gender gender { get; set; }
        #endregion
    }
}
