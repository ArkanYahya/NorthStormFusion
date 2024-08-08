using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using NuGet.Packaging.Signing;
using static Azure.Core.HttpHeader;

namespace NorthStorm.Models.Assistants
{
    public class AuditLog
    {
        #region Security Properties
        // *********************************** الخواص الأمنية للحقول  *************************  // 
        // يمكنك إستخدام مزود خدمة قواعد البيانات في توليد قيمة محسوبة للحقل، مثلا
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // ولكن هناك بعض القيم يجب ان تحتسب بشكل يدوي عن طريق توليدها في
        // Data\AppliationDbContext

        // من المفترض ان يتم حفظ وقت وتاريخ آخر تعديل
        // لكن يجب اعداد
        // Trigger
        // في قواعد البيانات
#warning set up a trigger in database to save record update timedate

        // لحفظ وقت وتاريخ انشاء القيد
        public DateTime Created { get; set; }

        // لحفظ وقت وتاريخ تعديل القيد
        public DateTime LastUpdated { get; set; }

        // لبيان حالة القيد
        // pending, approved, rejected
        public State State { get; set; }

        #endregion


        public int AuditID { get; set; }

        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }

        public string IPAddress { get; set; }

        public string ActionType { get; set; }

        public string PreviousValue { get; set; }

        public string NewValue { get; set; }

        public int RecordID { get; set; }
        public string RecordName { get; set; }

        public string DeviceID { get; set; }

        public string Note { get; set; }

    }
}
