using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;

using NorthStorm.Models.ViewModels;

// CreditedService s Controller
namespace NorthStorm.Controllers
{
    public class CreditedServicesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ICreditedService _CreditedServiceRepo;

        public CreditedServicesController(NorthStormContext context, ICreditedService creditedServiceRepo)
        {
            _context = context;
            _CreditedServiceRepo = creditedServiceRepo;
        }

        // GET: CreditedServices
        public async Task<IActionResult> Index(
            int? selectedId,
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("ReferenceNo");
            sortModel.AddColumn("ReferenceDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            if (selectedId != null)
                ViewData["CreditedServiceId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<CreditedService> items = await _CreditedServiceRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: CreditedServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditedService = await _CreditedServiceRepo.GetItem((int)id);

            if (creditedService == null)
            {
                return NotFound();
            }

            return View(creditedService);
        }


        // GET: CreditedService/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateCreditedServiceList();
            var item = new CreditedServiceCreateViewModel();
            return View(item);
        }








        // POST: CreditedService/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, OnCreditedServiceDate, UntilDate, CreditedServiceInDays, CreditedServiceInMonthes, CreditedServiceInYears, CreditedServiceForPromotionInDays, CreditedServiceForPromotionInMonthes, CreditedServiceForPromotionInYears, CreditedServiceForBounsInDays, CreditedServiceForBounsInMonthes, CreditedServiceForBounsInYears, CreditedServiceForRetirementInDays, CreditedServiceForRetirementInMonthes, CreditedServiceForRetirementInYears, CreditedServiceClassificationId, CreditedServiceId, EmployeeIds")] CreditedServiceCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";
                var creditedService = new CreditedService
                {


                    ReferenceNo = viewmodel.ReferenceNo,
                    ReferenceDate = viewmodel.ReferenceDate,
                    Subject = viewmodel.Subject,
                    OnCreditedServiceDate = viewmodel.OnCreditedServiceDate,
                    UntilDate = viewmodel.UntilDate,


                    CreditedServiceInDays = viewmodel.CreditedServiceInDays,
                    CreditedServiceInMonthes = viewmodel.CreditedServiceInMonthes,
                    CreditedServiceInYears = viewmodel.CreditedServiceInYears,


                    CreditedServiceForPromotionInDays = viewmodel.CreditedServiceForPromotionInDays,
                    CreditedServiceForPromotionInMonthes = viewmodel.CreditedServiceForPromotionInMonthes,
                    CreditedServiceForPromotionInYears = viewmodel.CreditedServiceForPromotionInYears,
                    CreditedServiceForBounsInDays = viewmodel.CreditedServiceForBounsInDays,
                    CreditedServiceForBounsInMonthes = viewmodel.CreditedServiceForBounsInMonthes,
                    CreditedServiceForBounsInYears = viewmodel.CreditedServiceForBounsInYears,
                    CreditedServiceForRetirementInDays = viewmodel.CreditedServiceForRetirementInDays,
                    CreditedServiceForRetirementInMonthes = viewmodel.CreditedServiceForRetirementInMonthes,
                    CreditedServiceForRetirementInYears = viewmodel.CreditedServiceForRetirementInYears,
                    CreditedServiceClassificationId = viewmodel.CreditedServiceClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };






                try
                {
                    IsCreated = await _CreditedServiceRepo.Create(creditedService);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _CreditedServiceRepo.GetErrors();

                    PopulateCreditedServiceList(creditedService.CreditedServiceClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CreditedServicesCreate_POST", errMessage);

                    return View(creditedService);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + creditedService.ReferenceNo + " المؤرخ في " + creditedService.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateCreditedServiceList(viewmodel.CreditedServiceClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: CreditedServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditedService = await _CreditedServiceRepo.GetItem((int)id);

            if (creditedService == null)
            {
                return NotFound();
            }

            //return View(creditedService);

            var model = new CreditedServiceEditViewModel
            {

                CreditedServiceId                 = creditedService.Id,
                ReferenceNo                       = creditedService.ReferenceNo,
                ReferenceDate                     = creditedService.ReferenceDate,
                Subject                           = creditedService.Subject,
                OnCreditedServiceDate             = creditedService.OnCreditedServiceDate,
                UntilDate                         = creditedService.UntilDate,
                CreditedServiceInDays             = creditedService.CreditedServiceInDays,
                CreditedServiceInMonthes          = creditedService.CreditedServiceInMonthes,
                CreditedServiceInYears            = creditedService.CreditedServiceInYears,


                CreditedServiceForPromotionInDays = creditedService.CreditedServiceForPromotionInDays,
                CreditedServiceForPromotionInMonthes = creditedService.CreditedServiceForPromotionInMonthes,
                CreditedServiceForPromotionInYears = creditedService.CreditedServiceForPromotionInYears,
                CreditedServiceForBounsInDays = creditedService.CreditedServiceForBounsInDays,
                CreditedServiceForBounsInMonthes = creditedService.CreditedServiceForBounsInMonthes,
                CreditedServiceForBounsInYears = creditedService.CreditedServiceForBounsInYears,
                CreditedServiceForRetirementInDays = creditedService.CreditedServiceForRetirementInDays,
                CreditedServiceForRetirementInMonthes = creditedService.CreditedServiceForRetirementInMonthes,
                CreditedServiceForRetirementInYears = creditedService.CreditedServiceForRetirementInYears,
                CreditedServiceClassificationId = creditedService.CreditedServiceClassificationId,
                Employees        = creditedService.Employees.Select(e => new EmployeesInfo7
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateCreditedServiceList(model.CreditedServiceId);
            return View(model);
        }

        // POST: CreditedServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreditedServiceId, ReferenceNo, ReferenceDate, Subject, OnCreditedServiceDate, UntilDate, CreditedServiceInDays, CreditedServiceInMonthes, CreditedServiceInYears, CreditedServiceForPromotionInDays, CreditedServiceForPromotionInMonthes, CreditedServiceForPromotionInYears, CreditedServiceForBounsInDays, CreditedServiceForBounsInMonthes, CreditedServiceForBounsInYears, CreditedServiceForRetirementInDays, CreditedServiceForRetirementInMonthes, CreditedServiceForRetirementInYears, CreditedServiceClassificationId, Employees")] CreditedServiceEditViewModel model)
        {
            if (id != model.CreditedServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var creditedService = await _context.CreditedServices
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (creditedService == null)
                    {
                        return NotFound();
                    }

                    creditedService.ReferenceNo = model.ReferenceNo;
                    creditedService.ReferenceDate = model.ReferenceDate;
                    creditedService.Subject = model.Subject;
                    creditedService.OnCreditedServiceDate = model.OnCreditedServiceDate;
                    creditedService.UntilDate             = model.UntilDate;
                    creditedService.CreditedServiceInDays = model.CreditedServiceInDays;
                    creditedService.CreditedServiceInMonthes = model.CreditedServiceInMonthes;
                    creditedService.CreditedServiceInYears = model.CreditedServiceInYears;
                    creditedService.CreditedServiceForPromotionInDays = model.CreditedServiceForPromotionInDays;
                    creditedService.CreditedServiceForPromotionInMonthes = model.CreditedServiceForPromotionInMonthes; ;
                    creditedService.CreditedServiceForPromotionInYears = model.CreditedServiceForRetirementInYears;
                    creditedService.CreditedServiceForBounsInDays = model.CreditedServiceForBounsInDays;
                    creditedService.CreditedServiceForBounsInMonthes = model.CreditedServiceForBounsInMonthes;
                    creditedService.CreditedServiceForBounsInYears = model.CreditedServiceForBounsInYears;
                    creditedService.CreditedServiceForRetirementInDays = model.CreditedServiceForRetirementInDays;
                    creditedService.CreditedServiceForRetirementInMonthes = model.CreditedServiceForRetirementInMonthes;
                    creditedService.CreditedServiceForRetirementInYears = model.CreditedServiceForRetirementInYears;
                    creditedService.CreditedServiceClassificationId = model.CreditedServiceClassificationId;

                    // Update the related employees
                    creditedService.Employees.Clear();
                    creditedService.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(creditedService);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _CreditedServiceRepo.Edit(creditedService);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _CreditedServiceRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CreditedServicesEdit_POST", errMessage);
                    PopulateCreditedServiceList(model.CreditedServiceId);
                    return View(model);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الأمر ذي العدد \n" + model.ReferenceNo + " المؤرخ في" + model.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";
                PopulateCreditedServiceList(model.CreditedServiceId);

                return View(model);
            }
        }

        // GET: CreditedServices/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditedService = await _CreditedServiceRepo.GetItem((int)id);

            if (creditedService == null)
            {
                return NotFound();
            }
            
            PopulateCreditedServiceList(creditedService.CreditedServiceClassificationId);

            return View(creditedService);
        }

        // POST: CreditedServices/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CreditedService creditedService)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string creditedServiceInfo = "";// creditedService.ReferenceNo + " في " + creditedService.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _CreditedServiceRepo.Delete(creditedService);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _CreditedServiceRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("CreditedServicesDelete_POST", errMessage);
                return View(creditedService);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + creditedServiceInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        // Autocomplete action
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            var employees = _context.Employees
                                     .Where(e => e.Id.ToString().Contains(term)||
                                          e.FirstName.Contains(term)||
                                          e.MiddleName.Contains(term)||
                                          e.LastName.Contains(term)||
                                          e.FourthName.Contains(term)||
                                          e.SurName.Contains(term)||
      e.GenderId.ToString().Contains(term) ||   // تمت الاضافة للحصول على مفتاح الجنس 1=ذكر

                                          (e.FirstName + " " + e.MiddleName + " " + e.LastName + " " + e.FourthName + " " + e.SurName).Contains(term)
                                    )
                                    .Select(e => new
                                    {
                                        value  = e.Id,
                                        name   = e.FullName,
                                        gender = e.GenderId,    // اضافة
                                        label  = e.Id + " - " + e.FullName
                                    })
                                    .ToList();
            return Json(employees);
        }

        private void PopulateCreditedServiceList(object selectedCreditedService = null)
        {
            var CreditedServicesQuery = from l in _context.CreditedServiceClassifications
                               orderby l.Name
                               select l;
            ViewBag.CreditedServiceClassificationIds = new SelectList(CreditedServicesQuery.AsNoTracking(), "Id", "Name", selectedCreditedService);

        }
    }

}
