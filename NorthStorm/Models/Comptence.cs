using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models

                                 // الأختصاصات العلمية   Comptence

{
    public class Comptence
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required, Display(Name = "التخصص")]
        public string Name { get; set; }

        #endregion

        #region Foreign Key

        //[Required(ErrorMessage = "يرجى ادخال رتبة التخصص عام/ دقيق"), Display(Name = "الرتبة")]
        //public string RankAsNumber { get; set; }

        [Display(Name = "التخصص العام")]
        public int? ParentComptenceId { get; set; }
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }
           
        [Display(Name = "رمز SPSS للاختصاصات")]
        public int? ComptenceSPSS { get; set; }
        #endregion
        #region Navigation Properties
        public Comptence ParentComptence { get; set; }
        public ICollection<Comptence> ChildComptences { get; set; }
        public ComptenceClassification Classification { get; set; }
        //public ICollection<Level> Levels { get; set; }

       #endregion
    }
}

