using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using NorthStorm.Models.Validations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models
{
    public class Employee
    {
        #region Class Properties
        // يستخدم ليكون الحقل مفتاح رئيسي في الجدول Key
        // فسيعرفه النظام تلقائيا كمفتاح رئيسي EmployeeId أو Id علما انه في حال كان اسم الحقل هو كلمة
        // يستخدم لعدم تفعيل الزيادة التلقائية في الحقل وانما  DatabaseGenerated(DatabaseGeneratedOption.None)  
        // يتم استخدام الادخال اليديوي 
        // يستخدم لتحديد الاسم الذي سيظهر فيه الحقل للمستخدم Display(Name =
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "رقم الموظف")]
        [EmployeeIdValidation]
        //[Unique(ErrorMessage = "رقم الموظف هذا موجود بالفعل !!")]
        public int Id { get; set; }

        // Required يستخدم للحقول المطلوبة ولا يمكن ادخال البيانات دون اعطاء قيمة لها
        // StringLength(50, MinimumLength = 3) تحديد اعلى واقل قيمة يمكن ادخالها في الحقل
        [Required(ErrorMessage = "يرجى ادخال الاسم الأول"), Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "يرجى ادخال الاسم الثاني"), Display(Name = "الاسم الثاني")]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "يرجى ادخال الاسم الثالث"), Display(Name = "الاسم الثالث")]
        public string LastName { get; set; }


        [Display(Name = "الاسم الرابع")]
        public string FourthName { get; set; }


        [Display(Name = "اللقب")]
        public string SurName { get; set; }


        [Display(Name = "اسم الام الاول")]
        public string MotherFirstName { get; set; }


        [Display(Name = "اسم الأم الثاني")]
        public string MotherMiddleName { get; set; }


        [Display(Name = "اسم الأم الثالث")]
        public string MotherLastName { get; set; }

        // يستخدم لعرض تاريخ الميلاد والذي نوعه تاريخ ووقت بنمط التاريخ فقط DataType.Date
        // يستخدم لتحديد نمط التاريخ يوم - شهر - سنة DisplayFormat(DataFormatString =
        // يستخدم لاستخدام النمط اثناء فتح صفة التعديل ايضا ApplyFormatInEditMode
        [DataType(DataType.Date), Display(Name = "التولد")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }


        // DisplayFormat(NullDisplayText =  يستخدم لإظهار قيمة للحقل في حال كانت قيمته غير معرفة
        [Display(Name = "الرقم المدني"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string CivilNumber { get; set; }


        [Display(Name = "رقم IBAN"), DisplayFormat(NullDisplayText = "غير مخصص")]
        public string IBAN { get; set; }

        // *****************************  الخصائص مع تخصيص دالة الاسناد ******************************
        // تمكنك هذه الخصائص من احتساب قيمة جديدة بالاعتماد على حقول الجدول الموجودة مسبقا
        // هذه الخاصية تدمج الحقول الخاصة بالاسم في حقل جديد دون الحاجة لفتح حقل جديد في قاعدة البيانات
        [Display(Name = "الاسم الكامل")]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName + " " + FourthName + " " + SurName;
            }
        }

        [Display(Name = "اسم الأم")]
        public string MotherName
        {
            get
            {
                return MotherFirstName + " " + MotherMiddleName + " " + MotherLastName;
            }
        }

        #endregion

        #region Foreign Keys
        // ******************************** Foreign Keys **************************************
        // وهي حقول المفاتيح الاساسية الخاصة بجداول اخرى
        // إن وجود مفتاح خارجي في النموذج على الرغم من وجود خاصية التنقل تسهل عملية التعديل وتجعلها اكثر كفائة
        // فعندما تقوم بتحميل موظف ليتم التعديل عليه، تكون قيمة خاصية التنقل للجنس غيرة معرفة ان لم تقم بتحميلها
        // لذا عليك تحميل الحقل الخاص من جدول الجنس اولا
        // بينما وجود المفتاح الخارجي في النموذج يمكنك من التعديل على الموظف دون تحميل كافة الحقول الخارجية
        public int? GenderId { get; set; }          // Foreign Key
        public int? ReligionId { get; set; }        // Foreign Key
        public int? RaceId { get; set; }            // Foreign Key
        public int? NationalityId { get; set; }     // Foreign Key
        public int? StatusId { get; set; }          // Foreign Key
        public int? RecruitmentId { get; set; }     // Foreign Key
        #endregion


        #region Navigation Properties
        // ****************************** Navigation Property خواص التنقل ******************************
        // وهي خواص تمكنك من تعريف العلاقات بين الجداول بالاعتماد على قيم المفاتيح الاساسية في الجدول الرئيسي
        // والمفاتيح الخارجية ( المفتيح الاساسية التابعة لجداول أخرى) ـ وهي اما تكون
        // One-To-One
        // One-To-Many
        // Many-To-Many

        // خاصية التنقل أدناه تثمل علاقة نوع
        // One-To-One
        // فكل موظف مرتبط بأمر إداري خاص بالتعيين واحد
        public Recruitment Recruitment { get; set; }
        public ICollection<EmployeeJobTitle> EmployeeJobTitles { get; set; }
        public ICollection<JobTransfer> JobTransfers { get; set; } = new Collection<JobTransfer>();
        public ICollection<Level> Levels { get; set; } = new Collection<Level>();
        public ICollection<Salary> Salaries { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
        public ICollection<Promotion> Promotions { get; set; }

        // خاصية التنقل الخاصة بالجنس وتمثل علاقة
        // One-To-Many
        // فلكل موظف جنس واحد ولكل جنس عدد من الموظفين
        [Display(Name = "الجنس")]
        public Gender gender { get; set; }

        // خاصية التنقل الخاصة بالديانة وتمثل علاقة
        // One-To-Many
        // فلكل موظف ديانة واحدة ولكل ديانة عدد من الموظفين
        [Display(Name = "الديانة")]
        public Religion religion { get; set; }

        // One-To-Many
        [Display(Name = "القومية")]
        public Race race { get; set; }

        // One-To-Many
        [Display(Name = "الجنسية")]
        public Nationality nationality { get; set; }

        // One-To-Many
        [Display(Name = "الحالة")]
        public Status status { get; set; }

        #endregion

        #region Not Mapped
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public bool IsCreateAction { get; set; } = true;
        #endregion
    }
}
