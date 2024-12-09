using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Controllers
{
    public class StaffingsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IStaffing _StaffingRepo;

        public StaffingsController(NorthStormContext context, IStaffing staffingRepo)
        {
            _context = context;
            _StaffingRepo = staffingRepo;
        }

        // GET: Staffings
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
                ViewData["StaffingId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Staffing> items = await _StaffingRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Staffings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffing = await _StaffingRepo.GetItem((int)id);

            if (staffing == null)
            {
                return NotFound();
            }

            return View(staffing);
        }


        // GET: Staffing/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateDropDownList();
            var item = new StaffingCreateViewModel();
            return View(item);
        }

        // POST: Staffing/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, StaffingCount, StaffingJobTitleId, StaffingUnitId,EmployeeCounted,VacantStaffing, EmployeeIds")] StaffingCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var staffing = new Staffing
                {
                    ReferenceNo        = model.ReferenceNo,
                    ReferenceDate      = model.ReferenceDate,
                    StaffingCount      = model.StaffingCount,
                    StaffingJobTitleId = model.StaffingJobTitleId,
                    StaffingUnitId     = model.StaffingUnitId,
                    EmployeeCounted  = model.EmployeeCounted,
                    VacantStaffing = model.VacantStaffing,
                    Employees = await _context.Employees
                                             .Where(e => model.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _StaffingRepo.Create(staffing);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _StaffingRepo.GetErrors();

                    PopulateDropDownList(model.StaffingJobTitleId, model.StaffingUnitId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StaffingsCreate_POST", errMessage);

                    return View(staffing);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + staffing.ReferenceNo + " المؤرخ في " + staffing.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateDropDownList(model.StaffingJobTitleId, model.StaffingUnitId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(model);
            }
        }


        // GET: Staffings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffing = await _StaffingRepo.GetItem((int)id);

            if (staffing == null)
            {
                return NotFound();
            }

            //return View(staffing);

            var model = new StaffingEditViewModel
            {
                StaffingId         = staffing.Id,
                ReferenceNo        = staffing.ReferenceNo,
                ReferenceDate      = staffing.ReferenceDate,
                StaffingCount      = staffing.StaffingCount,
                StaffingJobTitleId = staffing.StaffingJobTitleId,
                StaffingUnitId     = staffing.StaffingUnitId,
                EmployeeCounted = staffing.EmployeeCounted,
                VacantStaffing = staffing.VacantStaffing,

                Employees = staffing.Employees.Select(e => new EmployeesInfo11
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateDropDownList(model.StaffingJobTitleId);
            return View(model);
        }

        // POST: Staffings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffingId, ReferenceNo, ReferenceDate, StaffingCount, StaffingJobTitleId, StaffingUnitId,EmployeeCounted,VacantStaffing, Employees")] StaffingEditViewModel model)
        {
            if (id != model.StaffingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var staffing = await _context.Staffings
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (staffing == null)
                    {
                        return NotFound();
                    }

                    staffing.ReferenceNo = model.ReferenceNo;
                    staffing.ReferenceDate = model.ReferenceDate;
                    staffing.StaffingCount = model.StaffingCount;
                    staffing.StaffingJobTitleId = model.StaffingJobTitleId;
                    staffing.StaffingUnitId = model.StaffingUnitId;
                    staffing.EmployeeCounted = model.EmployeeCounted;
                    staffing.VacantStaffing = model.VacantStaffing;


                    // Update the related employees
                    staffing.Employees.Clear();
                    staffing.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(staffing);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _StaffingRepo.Edit(staffing);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _StaffingRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StaffingsEdit_POST", errMessage);
                    PopulateDropDownList(model.StaffingJobTitleId, model.StaffingUnitId);
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
                PopulateDropDownList(model.StaffingJobTitleId, model.StaffingUnitId);

                return View(model);
            }
        }

        // GET: Staffings/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffing = await _StaffingRepo.GetItem((int)id);

            if (staffing == null)
            {
                return NotFound();
            }
            
            PopulateDropDownList(staffing.StaffingJobTitle);

            return View(staffing);
        }

        // POST: Staffings/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Staffing staffing)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string staffingInfo = "";// staffing.ReferenceNo + " في " + staffing.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _StaffingRepo.Delete(staffing);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _StaffingRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("StaffingsDelete_POST", errMessage);
                return View(staffing);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + staffingInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        // Autocomplete action
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            var employees = _context.Employees
                                     .Where(e => e.Id.ToString().Contains(term) ||
                                          e.FirstName.Contains(term) ||
                                          e.MiddleName.Contains(term) ||
                                          e.LastName.Contains(term) ||
                                          e.FourthName.Contains(term) ||
                                          e.SurName.Contains(term) ||
                                          (e.FirstName + " " + e.MiddleName + " " + e.LastName + " " + e.FourthName + " " + e.SurName).Contains(term)
                                    )
                                    .Select(e => new
                                    {
                                        value = e.Id,
                                        name = e.FullName,
                                        label = e.Id + " - " + e.FullName
                                    })
                                    .ToList();
            return Json(employees);
        }


        //private void PopulateDropDownLists(
        //   object selectedGender = null,
        //   object selectedNationality = null,

        private void PopulateDropDownList(object selectedstaffingjobtitle = null,
                                          object selectedstaffingunit = null)
        {

            var staffingJobtitleQuery = from l in _context.JobTitles
                              orderby l.Name
                              select l;
            ViewBag.StaffingJobTitleId = new SelectList(staffingJobtitleQuery.AsNoTracking(), "Id", "Name", selectedstaffingjobtitle);


            var staffingUnitQuery = from l in _context.Levels
                               orderby l.Name
                               select l;
            ViewBag.StaffingUnitId = new SelectList(staffingUnitQuery.AsNoTracking(), "Id", "Name", selectedstaffingunit);

        }
    }

}
