using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using NorthStorm.Models.Validations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models
{
    public class NationalCard       // البطاقة الوطنية الموحدة
    {
        #region Class Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "رقم الموظف")]
        //[Unique(ErrorMessage = "رقم الموظف هذا موجود بالفعل !!")]
        public int Id { get; set; }

        //       معلومات البطاقة الموحــــــدة
        [Range(0, 999999999999)]
        [UIHint("رقم البطاقة الموحدة منتصف البطاقة و أعلى الصورة ")]
        [Required(ErrorMessage = "يرجى ادخال رقم اليطاقة الوطنية")]
        [Display(Name = "رقم البطاقة الوطنية"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int NationalCardNumber { get; set; }

        [UIHint("رقم البطاقة الوطنية الرقم اسفل الصورة ")]
        [Required(ErrorMessage = "يرجى ادخال رقم اليطاقة الوطنية اسفل الصورة")]
        [Display(Name = "الرقم اسفل الصورة"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string CivilStatusIdNumber { get; set; }

        [UIHint("الرقم العائلي اخر رقم في ظهر البطاقة ")]
        [Required(ErrorMessage = "يرجى ادخال الرقم العائلي")]
        [Display(Name = "رقم البطاقة الوطنية"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string FamilyIdNumber { get; set; }

        [UIHint("تاريخ إصدار البطاقة الوطنية ")]
        [Required, DataType(DataType.Date), Display(Name = "تاريخ الاصدار")]
        public DateTime NationalIdReleaseDate { get; set; } = DateTime.Now;

        [UIHint("تاريخ نفاذ البطاقة الوطنية ")]
        [Required, DataType(DataType.Date), Display(Name = "تاريخ النفاذ")]
        public DateTime NationalIdExpiryDate { get; set; } = DateTime.Now;
        #endregion
        
        


        #region Foreign Keys
        // ******************************** Foreign Keys **************************************
        // وهي حقول المفاتيح الاساسية الخاصة بجداول اخرى

        [UIHint("محل الولادة")]
        //   [Required(ErrorMessage = "يرجى ادخال محل الولادة/ مسقط الراس")]
        [Display(Name = "محل الولادة"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? PlaceOfBirthId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        #endregion

        #region Navigation Properties
        // ****************************** Navigation Property خواص التنقل ******************************
        public Location PlaceOfBirth { get; set; }           // محل الولادة
        public Employee Employee { get; set; }
        #endregion


        #region Not Mapped
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public bool IsCreateAction { get; set; } = true;
        #endregion
    }
}
