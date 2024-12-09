using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class NationalCardEditViewModel
    {

        public int EmployeeId { get; set; }

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

        [UIHint("محل الولادة")]
        [Display(Name = "محل الولادة"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? PlaceOfBirthId { get; set; }
        public Location Location { get; set; }
        

        public Employee Employee { get; set; }
    }

    public class EmployeesInfo13
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
