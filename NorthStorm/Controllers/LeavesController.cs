using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Leaves Controller
namespace NorthStorm.Controllers
{
    public class LeavesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ILeave _LeaveRepo;

        public LeavesController(NorthStormContext context, ILeave leaveRepo)
        {
            _context = context;
            _LeaveRepo = leaveRepo;
        }

        // GET: Leaves
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
                ViewData["LeaveId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Leave> items = await _LeaveRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _LeaveRepo.GetItem((int)id);

            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }


        // GET: Leave/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateLeaveList();
            var item = new LeaveCreateViewModel();
            return View(item);
        }

        // POST: Leave/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, LeaveReason, LeaveClassificationId, LeaveId,LeaveInDays, LeaveInMonthes,LeaveInYears,SupposedEnrollDate, OnLeaveDate, EnrollDate, EmployeeIds")] LeaveCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var leave = new Leave
                {
                    ReferenceNo              = viewmodel.ReferenceNo,
                    ReferenceDate            = viewmodel.ReferenceDate,
                    Subject                  = viewmodel.Subject,
                    LeaveReason           = viewmodel.LeaveReason,
                    LeaveInDays           = viewmodel.LeaveInDays,
                    LeaveInYears          = viewmodel.LeaveInYears,
                    LeaveInMonthes        = viewmodel.LeaveInMonthes,
                    SupposedEnrollDate       = viewmodel.OnLeaveDate.AddYears(viewmodel.LeaveInYears).AddMonths(viewmodel.LeaveInMonthes).AddDays(viewmodel.LeaveInDays),
                    OnLeaveDate              = viewmodel.OnLeaveDate,
                    EnrollDate               = viewmodel.EnrollDate,
                    LeaveClassificationId    = viewmodel.LeaveClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _LeaveRepo.Create(leave);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _LeaveRepo.GetErrors();

                    PopulateLeaveList(leave.LeaveClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LeavesCreate_POST", errMessage);

                    return View(leave);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + leave.ReferenceNo + " المؤرخ في " + leave.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateLeaveList(viewmodel.LeaveClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _LeaveRepo.GetItem((int)id);

            if (leave == null)
            {
                return NotFound();
            }

            //return View(leave);

            var model = new LeaveEditViewModel
            {
                LeaveId            = leave.Id,
                ReferenceNo           = leave.ReferenceNo,
                ReferenceDate         = leave.ReferenceDate,
                Subject               = leave.Subject,
                LeaveReason        = leave.LeaveReason,
                OnLeaveDate           = leave.OnLeaveDate,
                EnrollDate            = (DateTime)leave.EnrollDate,
                LeaveInDays        = leave.LeaveInDays,
                LeaveInMonthes     = leave.LeaveInMonthes,
                LeaveInYears       = leave.LeaveInYears,
                SupposedEnrollDate    = leave.OnLeaveDate.AddYears(leave.LeaveInYears).AddMonths(leave.LeaveInMonthes).AddDays(leave.LeaveInDays),
                LeaveClassificationId = leave.LeaveClassificationId, 
                Employees        = leave.Employees.Select(e => new EmployeesInfo3
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateLeaveList(model.LeaveId);
            return View(model);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveId, ReferenceNo, ReferenceDate, Subject, LeaveReason, LeaveClassificationId, LeaveInDays, LeaveInMonthes, LeaveInYears,SupposedEnrollDate, OnLeaveDate, EnrollDate, Employees")] LeaveEditViewModel model)
        {
            if (id != model.LeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var leave = await _context.Leaves
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (leave == null)
                    {
                        return NotFound();
                    }

                    leave.ReferenceNo           = model.ReferenceNo;
                    leave.ReferenceDate         = model.ReferenceDate;
                    leave.Subject               = model.Subject;
                    leave.LeaveReason        = model.LeaveReason;
                    leave.LeaveInDays        = model.LeaveInDays;
                    leave.LeaveInMonthes     = model.LeaveInMonthes;
                    leave.LeaveInYears       = model.LeaveInYears;
                    leave.SupposedEnrollDate    = model.OnLeaveDate.AddYears(leave.LeaveInYears).AddMonths(leave.LeaveInMonthes).AddDays(leave.LeaveInDays); ;
                    leave.OnLeaveDate           = model.OnLeaveDate;
                    leave.EnrollDate            = model.EnrollDate;
                    leave.LeaveClassificationId = model.LeaveClassificationId;

                    // Update the related employees
                    leave.Employees.Clear();
                    leave.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(leave);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _LeaveRepo.Edit(leave);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _LeaveRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LeavesEdit_POST", errMessage);
                    PopulateLeaveList(model.LeaveId);
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
                PopulateLeaveList(model.LeaveId);

                return View(model);
            }
        }

        // GET: Leaves/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _LeaveRepo.GetItem((int)id);

            if (leave == null)
            {
                return NotFound();
            }
            
            PopulateLeaveList(leave.LeaveClassificationId);

            return View(leave);
        }

        // POST: Leaves/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Leave leave)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string leaveInfo = "";// leave.ReferenceNo + " في " + leave.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _LeaveRepo.Delete(leave);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _LeaveRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("LeavesDelete_POST", errMessage);
                return View(leave);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + leaveInfo + " بنجاح";
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

        private void PopulateLeaveList(object selectedLeave = null)
        {
            var LeavesQuery = from l in _context.LeaveClassifications
                               orderby l.Name
                               select l;
            ViewBag.LeaveClassificationIds = new SelectList(LeavesQuery.AsNoTracking(), "Id", "Name", selectedLeave);

        }
    }

}
