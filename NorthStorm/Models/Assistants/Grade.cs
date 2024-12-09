using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace NorthStorm.Models.Assistants
{
    public class Grade         // جدول الرواتب والدرجات
    {
        #region Model Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الدرجة رقما"),
         Display(Name = "الدرجة رقما")]
        public int GradeNumber { get; set; }

        //[StringLength(13, MinimumLength = 4)]
        [Required(ErrorMessage = "يرجى ادخال الدرجة كتابة"),
        Display(Name = "الدرجة كتابة")]
        public string GradeAsWriting { get; set; }

        [Range(170000, 910000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الأولى"),
        Display(Name = "المرحلة01")]
        public int Stage01 { get; set; }

        [Range(173000, 930000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الثانية"),
        Display(Name = "المرحلة02")]
        public int Stage02 { get; set; }

        [Range(176000, 950000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الثالثة"),
        Display(Name = "المرحلة03")]
        public int Stage03 { get; set; }

        [Range(179000, 970000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الرابعة"),
        Display(Name = "المرحلة04")]
        public int Stage04 { get; set; }

        [Range(182000, 990000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الخامسة"),
         Display(Name = "المرحلة05")]
        public int Stage05 { get; set; }

        [Range(185000, 1010000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة السادسىة"),
            Display(Name = "المرحلة06")]
        public int Stage06 { get; set; }

        [Range(188000, 1030000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة السابعة"),
        Display(Name = "المرحلة07")]
        public int Stage07 { get; set; }

        [Range(191000, 1050000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الثامنة"),
        Display(Name = "المرحلة08")]
        public int Stage08 { get; set; }

        [Range(194000, 1070000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة التاسعة"),
         Display(Name = "المرحلة09")]
        public int Stage09 { get; set; }

        [Range(197000, 1090000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة العاشرة"),
         Display(Name = "المرحلة10")]
        public int Stage10 { get; set; }

        [Range(200000, 1110000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ المرحلة الحادية عشر"),
        Display(Name = "المرحلة11")]
        public int Stage11 { get; set; }

        [Range(3000, 20000)]
        [UIHint("NumberInThousands")]
        [Required(ErrorMessage = "يرجى ادخال مبلغ العلاوة السنوية"),
         Display(Name = " العلاوة السنوية")]
        public int AnnualBonus { get; set; }

        //	[Range(0, 9)]
        [Required(ErrorMessage = "يرجى ادخال المدة الاصغرية للترقية"),
        Display(Name = "مدة الترقية")]
        public int MinimumDuration { get; set; }
        #endregion

        #region Foreign Keys
        public string? RankAsFemale { get; set; }          // Foreign Key
        #endregion

        #region Navigstion Properties
        [Display(Name = "الدرجة")]
        public RankOther rankOther { get; set; }

        //public string RankAsFemale { get; set; }   المطلوب هو الرانك فيميل الاولى ...الخ
        #endregion
    }
}
