using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class CreditedServiceEditViewModel
    {
        public int CreditedServiceId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }


        //public CreditedServiceClassification CreditedServiceClassification { get; set; }
        public List<int> EmployeeIds { get; set; }

        //[Display(Name = "نوع الخدمة المضافة")]
        //public int? CreditedServiceClassificationId { get; set; }

        [DataType(DataType.Date), Display(Name = "إعتبارا من")]
        public DateTime OnCreditedServiceDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date), Display(Name = "لغاية")]
        public DateTime UntilDate { get; set; } = DateTime.Now;

      
        [Required, Display(Name = "يوم")]
        public int CreditedServiceInDays { get; set; }
     
        [Required, Display(Name = "شهر")]
        public int CreditedServiceInMonthes { get; set; }
      
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
            CreditedServiceInYears = years;
            CreditedServiceInMonthes = months;
            CreditedServiceInDays = days;

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

        [Display(Name = "نوع الخدمة المضافة")]
        public int? CreditedServiceClassificationId { get; set; }
        public CreditedServiceClassification CreditedServiceClassification { get; set; }
        public List<EmployeesInfo7> Employees { get; set; } = new List<EmployeesInfo7>();
    }

    public class EmployeesInfo7
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
