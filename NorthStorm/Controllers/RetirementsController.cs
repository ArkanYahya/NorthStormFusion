using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Retirement s Controller
namespace NorthStorm.Controllers
{
    public class RetirementsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IRetirement _RetirementRepo;

        public RetirementsController(NorthStormContext context, IRetirement retirementRepo)
        {
            _context = context;
            _RetirementRepo = retirementRepo;
        }
        // GET: Retirements
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
                ViewData["RetirementId"] = selectedId.Value;
            ViewBag.SearchText = SearchText;
            PaginatedList<Retirement> items = await _RetirementRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Retirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var retirement = await _RetirementRepo.GetItem((int)id);
            if (retirement == null)
            {
                return NotFound();
            }
            return View(retirement);
        }
       // GET: Retirement/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateRetirementList();
            var item = new RetirementCreateViewModel();
            return View(item);
        }
        // POST: Retirement/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, DismissClassificationId, StatusId, RetirementId, DisengagementDate, EmployeeIds")] RetirementCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";
                var retirement = new Retirement
                {
                    ReferenceNo              = viewmodel.ReferenceNo,
                    ReferenceDate            = viewmodel.ReferenceDate,
                    Subject                  = viewmodel.Subject,
                    DisengagementDate        = viewmodel.DisengagementDate,
                    DismissClassificationId  = viewmodel.DismissClassificationId,
       //             StatusId = viewmodel.StatusId
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };
                try
                {
                    IsCreated = await _RetirementRepo.Create(retirement);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _RetirementRepo.GetErrors();
                    PopulateRetirementList(retirement.DismissClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RetirementsCreate_POST", errMessage);
                    return View(retirement);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + retirement.ReferenceNo + " المؤرخ في " + retirement.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateRetirementList(viewmodel.DismissClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }
        // GET: Retirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var retirement = await _RetirementRepo.GetItem((int)id);
            if (retirement == null)
            {
                return NotFound();
            }

            //return View(retirement);
            var model = new RetirementEditViewModel
            {
                RetirementId            = retirement.Id,
                ReferenceNo           = retirement.ReferenceNo,
                ReferenceDate         = retirement.ReferenceDate,
                Subject               = retirement.Subject,
                DisengagementDate = (DateTime)retirement.DisengagementDate,
                DismissClassificationId = retirement.DismissClassificationId, 
                Employees        = retirement.Employees.Select(e => new EmployeesInfo9
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateRetirementList(model.RetirementId);
            return View(model);
        }
        // POST: Retirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RetirementId, ReferenceNo, ReferenceDate, Subject, DismissClassificationId, StatusId, DisengagementDate, Employees")] RetirementEditViewModel model)
        {
            if (id != model.RetirementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var retirement = await _context.Retirements
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);
                    if (retirement == null)
                    {
                        return NotFound();
                    }
                    retirement.ReferenceNo           = model.ReferenceNo;
                    retirement.ReferenceDate         = model.ReferenceDate;
                    retirement.Subject               = model.Subject;
                    retirement.DisengagementDate = model.DisengagementDate;
                    retirement.DismissClassificationId = model.DismissClassificationId;
                    // Update the related employees
                    retirement.Employees.Clear();
                    retirement.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(retirement);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _RetirementRepo.Edit(retirement);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _RetirementRepo.GetErrors();
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RetirementsEdit_POST", errMessage);
                    PopulateRetirementList(model.RetirementId);
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
                PopulateRetirementList(model.RetirementId);
                return View(model);
            }
        }
        // GET: Retirements/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var retirement = await _RetirementRepo.GetItem((int)id);
            if (retirement == null)
            {
                return NotFound();
            }
            PopulateRetirementList(retirement.DismissClassificationId);
            return View(retirement);
        }

        // POST: Retirements/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Retirement retirement)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string retirementInfo = "";// retirement.ReferenceNo + " في " + retirement.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _RetirementRepo.Delete(retirement);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _RetirementRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("RetirementsDelete_POST", errMessage);
                return View(retirement);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + retirementInfo + " بنجاح";
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
      e.BirthDate.ToString().Contains(term)||

                                          (e.FirstName + " " + e.MiddleName + " " + e.LastName + " " + e.FourthName + " " + e.SurName).Contains(term)
                                    )
                                    .Select(e => new
                                    {
                                        value  = e.Id,
                                        name   = e.FullName,
                                        gender = e.GenderId,         // اضافة
                                        birthdate = e.BirthDate,      // اضافة
                                        label  = e.Id + " - " + e.FullName
                                    })
                                    .ToList();
            return Json(employees);
        }
        private void PopulateRetirementList(object selectedRetirement = null)
        {
            var RetirementsQuery = from l in _context.DismissClassifications
                                   orderby l.Name
                               select l;
            ViewBag.RetirementClassificationIds = new SelectList(RetirementsQuery.AsNoTracking(), "Id", "Name", selectedRetirement);

        }
    }

}
