using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class CreditedService
   
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        //[Display(Name = "نوع الخدمة المضافة")]
        //public int? CreditedServiceClassificationId { get; set; }

        [DataType(DataType.Date), Display(Name = "إعتبارا من")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime OnCreditedServiceDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date), Display(Name = "لغاية")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime UntilDate { get; set; }

     //   [Range(0,30)]
        [Required, Display(Name = "يوم")]
        public int CreditedServiceInDays { get; set; }

     //   [Range(0, 11)]
        [Required, Display(Name = "شهر")]
        public int CreditedServiceInMonthes { get; set; }

     //   [Range(0, 5)]
        [Required, Display(Name = "سنة")]
        public int CreditedServiceInYears { get; set; }

        //   لحساب المدة بالسنوات و الاشهر و الايام تلقائيا
        public void CalculateServiceDifference()
        {
            DateTime startDate = OnCreditedServiceDate;
            DateTime endDate = UntilDate;
            int years = endDate.Year - startDate.Year;
            int months = endDate.Month - startDate.Month;
            int days = endDate.Day - startDate.Day;
            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(startDate.Year, startDate.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }
            CreditedServiceInYears   = years;
            CreditedServiceInMonthes = months;
            CreditedServiceInDays    = days;
        }
    


//  هنا يتم احتساب المدة الكليةالمضافة والمحتسبة للترقية بشكل تلقائي
[Display(Name = "يوم")]
        public int CreditedServiceForPromotionInDays { get; set; }
        [Display(Name = "شهر")]
        public int CreditedServiceForPromotionInMonthes { get; set; }
        [Display(Name = "سنة")]
        public int CreditedServiceForPromotionInYears { get; set; }

        //  هنا يتم احتساب المدة الكليةالمضافة والمحتسبة للعلاوة بشكل تلقائي
        [Display(Name = "يوم")]
        public int CreditedServiceForBounsInDays { get; set; }
        [Display(Name = "شهر")]
        public int CreditedServiceForBounsInMonthes { get; set; }
        [Display(Name = "سنة")]
        public int CreditedServiceForBounsInYears { get; set; }

        //  هنا يتم احتساب المدة الكليةالمضافة والمحتسبة للتقاعد بشكل تلقائي
        [Display(Name = "يوم")]
        public int CreditedServiceForRetirementInDays { get; set; }
        [Display(Name = "شهر")]
        public int CreditedServiceForRetirementInMonthes { get; set; }
        [Display(Name = "سنة")]
        public int CreditedServiceForRetirementInYears { get; set; }
        #endregion

        #region Foreign Key
        [Display(Name = "نوع الخدمة المضافة")]
        public int? CreditedServiceClassificationId { get; set; }
        #endregion

        #region Navigation Properties
        public CreditedServiceClassification CreditedServiceClassification { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
