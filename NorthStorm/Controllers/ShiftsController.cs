using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Shifts Controller
namespace NorthStorm.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IShift _ShiftRepo;

        public ShiftsController(NorthStormContext context, IShift shiftRepo)
        {
            _context = context;
            _ShiftRepo = shiftRepo;
        }
        // GET: Shifts
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
                ViewData["ShiftId"] = selectedId.Value;
            ViewBag.SearchText = SearchText;
            PaginatedList<Shift> items = await _ShiftRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shift = await _ShiftRepo.GetItem((int)id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }
       // GET: Shift/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateShiftList();
            var item = new ShiftCreateViewModel();
            return View(item);
        }
        // POST: Shift/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, ShiftClassificationId, ShiftId, EnrollDate, EmployeeIds")] ShiftCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";
                var shift = new Shift
                {
                    ReferenceNo              = viewmodel.ReferenceNo,
                    ReferenceDate            = viewmodel.ReferenceDate,
                    Subject                  = viewmodel.Subject,
                    EnrollDate               = viewmodel.EnrollDate,
                    ShiftClassificationId    = viewmodel.ShiftClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };
                try
                {
                    IsCreated = await _ShiftRepo.Create(shift);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ShiftRepo.GetErrors();
                    PopulateShiftList(shift.ShiftClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ShiftsCreate_POST", errMessage);
                    return View(shift);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + shift.ReferenceNo + " المؤرخ في " + shift.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateShiftList(viewmodel.ShiftClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }
        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shift = await _ShiftRepo.GetItem((int)id);
            if (shift == null)
            {
                return NotFound();
            }

            //return View(shift);
            var model = new ShiftEditViewModel
            {
                ShiftId            = shift.Id,
                ReferenceNo           = shift.ReferenceNo,
                ReferenceDate         = shift.ReferenceDate,
                Subject               = shift.Subject,
                EnrollDate            = (DateTime)shift.EnrollDate,
                ShiftClassificationId = shift.ShiftClassificationId, 
                Employees        = shift.Employees.Select(e => new EmployeesInfo6
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateShiftList(model.ShiftId);
            return View(model);
        }
        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShiftId, ReferenceNo, ReferenceDate, Subject, ShiftClassificationId, EnrollDate, Employees")] ShiftEditViewModel model)
        {
            if (id != model.ShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var shift = await _context.Shifts
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);
                    if (shift == null)
                    {
                        return NotFound();
                    }
                    shift.ReferenceNo           = model.ReferenceNo;
                    shift.ReferenceDate         = model.ReferenceDate;
                    shift.Subject               = model.Subject;
                    shift.EnrollDate            = model.EnrollDate;
                    shift.ShiftClassificationId = model.ShiftClassificationId;
                    // Update the related employees
                    shift.Employees.Clear();
                    shift.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(shift);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _ShiftRepo.Edit(shift);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ShiftRepo.GetErrors();
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ShiftsEdit_POST", errMessage);
                    PopulateShiftList(model.ShiftId);
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
                PopulateShiftList(model.ShiftId);
                return View(model);
            }
        }
        // GET: Shifts/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shift = await _ShiftRepo.GetItem((int)id);
            if (shift == null)
            {
                return NotFound();
            }
            PopulateShiftList(shift.ShiftClassificationId);
            return View(shift);
        }

        // POST: Shifts/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Shift shift)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string shiftInfo = "";// shift.ReferenceNo + " في " + shift.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _ShiftRepo.Delete(shift);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ShiftRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ShiftsDelete_POST", errMessage);
                return View(shift);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + shiftInfo + " بنجاح";
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
        private void PopulateShiftList(object selectedShift = null)
        {
            var ShiftsQuery = from l in _context.ShiftClasifications
                               orderby l.Name
                               select l;
            ViewBag.ShiftClassificationIds = new SelectList(ShiftsQuery.AsNoTracking(), "Id", "Name", selectedShift);

        }
    }

}
