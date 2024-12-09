using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace NorthStorm.Models.Assistants
{
    public class RankOther    // نموذج للمساعدة في ترتيب الملفات تصاعديا او تنازليا او/و ترتيب ال بارنت و ال جايلد
    {
      
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Range(0, 20)]
        [Required, Display(Name = "الرتبة رقما")]
        public int RankAsNumber { get; set; }

        [Required(ErrorMessage = "اضافة الرتبة مع رقم مثل الرتبة1 الرتبة2 ... الخ"), Display(Name = "الرتبة")]
        public string RankAsWriting { get; set; }

        [Required(ErrorMessage = "اضافة الرتبة بشكل مؤنث الاولى، الثانية ... الخ"), Display(Name = "الرتبة كتابة مؤنث")]
        public string RankAsFemale { get; set; }


        [Required(ErrorMessage = "اضافة الرتبة بشكل مذكر الاول، الثاني ... الخ"), Display(Name = "الرتبة كتابة مذكر")]
        public string RankAsMale { get; set; }

        [Required(ErrorMessage = "اضافة الرتبة بشكل مراحل - المرحلة الاولى ، الثانية ...الخ"), Display(Name = "المراحل")]
        public string RankAsDistinction { get; set; }

        [Required(ErrorMessage = "اضافة الرتبة بشكل العام و الدقيق"), Display(Name = "الدرجات")]
        public string RankAsGeneral { get; set; }


        #endregion

        #region Navigation Properties
        //public ICollection<Comptence> Comptences { get; set; }
      //  public ICollection<Level> Levels { get; set; }


        #endregion
    }
}
