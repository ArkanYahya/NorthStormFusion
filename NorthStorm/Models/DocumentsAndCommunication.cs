using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using NorthStorm.Models.Validations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models
{
    public class DocumintsAndComminication
    {
        #region Class Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "رقم الموظف")]
        [EmployeeIdValidation]
        //[Unique(ErrorMessage = "رقم الموظف هذا موجود بالفعل !!")]
        public int Id { get; set; }


        //          معلومات البطاقة التموينية   

        [UIHint("رقم البطاقة التموينية ")]
     //   [Required(ErrorMessage = "يرجى ادخال رقم اليطاقة التموينية")]
        [Display(Name = "رقم البطاقة التموينية"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int RationIdNumber { get; set; }

        [UIHint("رقم المركز التمويني ")]
       // [Required(ErrorMessage = "يرجى ادخال رقم المركز التمويني")]
        [Display(Name = "رقم المركز التمويني"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int RationCenterNumber { get; set; }


       

        [UIHint("رقم الدار")]
        [Display(Name = "رقم الدار"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string HouseNo { get; set; }


        [UIHint("رقم الهاتف النقال ")]
       // [Required(ErrorMessage = "يرجى ادخال رقم الهاتف النقال")]
        [Display(Name = "رقم الموبايل"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int MobileNumber { get; set; }

        [UIHint("رقم الهاتف النفطي ")]
        [Display(Name = "رقم الهاتف النفطي"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int OilPhoneNumber { get; set; }

        [UIHint("البريد الالكتروني ")]
        [Display(Name = "البريد الالكتروني"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string EmployeeEmail { get; set; }



        #endregion

        #region Foreign Keys
        // ******************************** Foreign Keys **************************************
        // وهي حقول المفاتيح الاساسية الخاصة بجداول اخرى
        // إن وجود مفتاح خارجي في النموذج على الرغم من وجود خاصية التنقل تسهل عملية التعديل وتجعلها اكثر كفائة
        // بينما وجود المفتاح الخارجي في النموذج يمكنك من التعديل على الموظف دون تحميل كافة الحقول الخارجية

        public int? EmployeeId { get; set; }                        // Foreign Key

        [UIHint("اسم المركز التمويني ")]
        //  [Required(ErrorMessage = "يرجى ادخال اسم المركز التمويني")]
        [Display(Name = "المركز التمويني"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? RationCenterId { get; set; }


        //  عنوان السكن محافظة قضاء .. الخ

        [UIHint("الدولة")]
   //     [Required(ErrorMessage = "يرجى ادخال اسم الدولة")]
        [Display(Name = "الدولة"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? CountryAddressId { get; set; }

        [UIHint("المحافظة")]
        [Display(Name = "المحافظة"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? GovernorateAddressId { get; set; }

        [UIHint("القضاء")]
        [Display(Name = "القضاء"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? CityAddressId { get; set; }

        [UIHint("المنطقة")]
        [Display(Name = "المنظقة/الناحية"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? TownAddressId { get; set; }

        [UIHint("الحي")]
        [Display(Name = "الحي/القرية"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public int? NeighbrohoodAddressId { get; set; }

        #endregion


        #region Navigation Properties
        // ****************************** Navigation Property خواص التنقل ******************************

        public Location RationCenter { get; set; }           // اسم المركز التمويني
        public Location CountryAddress { get; set; }         // عنوان الدولة
        public Location GovernorateAddress { get; set; }     // المحافظة
        public Location CityAddress { get; set; }            // المدينة/القضاء
        public Location TownAddress { get; set; }            // المنطقة/ القرية 
        public Location NeighbrohoodAddress { get; set; }    // الحي السكني

        #endregion

        #region Not Mapped
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public bool IsCreateAction { get; set; } = true;
        #endregion
    }
}
