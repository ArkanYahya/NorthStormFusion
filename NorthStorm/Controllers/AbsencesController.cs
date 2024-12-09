using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Absences Controller
namespace NorthStorm.Controllers
{
    public class AbsencesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IAbsence _AbsenceRepo;

        public AbsencesController(NorthStormContext context, IAbsence absenceRepo)
        {
            _context = context;
            _AbsenceRepo = absenceRepo;
        }

        // GET: Absences
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
                ViewData["AbsenceId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Absence> items = await _AbsenceRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Absences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _AbsenceRepo.GetItem((int)id);

            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }


        // GET: Absence/CreateMasterDetails
        public IActionResult Create()
        {
           // PopulateAbsenceList();
            var item = new AbsenceCreateViewModel();

            return View(item);
        }

        // POST: Absence/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, AbsenceReason, AbsenceInDays, EnrollDate, OnAbsenceDate, EmployeeIds")] AbsenceCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var absence = new Absence
                {
                    ReferenceNo             = viewmodel.ReferenceNo,
                    ReferenceDate = viewmodel.ReferenceDate,
                    Subject                 = viewmodel.Subject,
                    AbsenceReason           = viewmodel.AbsenceReason,
                    AbsenceInDays           = (viewmodel.EnrollDate - viewmodel.OnAbsenceDate ).Days, //viewmodel.AbsenceInDays,
                    OnAbsenceDate           = viewmodel.OnAbsenceDate,
                    EnrollDate              = viewmodel.EnrollDate,
                    //AbsenceClassificationId    = viewmodel.AbsenceClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _AbsenceRepo.Create(absence);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _AbsenceRepo.GetErrors();

                 //   PopulateAbsenceList(absence.AbsenceClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("AbsencesCreate_POST", errMessage);

                    return View(absence);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + absence.ReferenceNo + " المؤرخ في " + absence.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                //PopulateAbsenceList(viewmodel.AbsenceClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _AbsenceRepo.GetItem((int)id);

            if (absence == null)
            {
                return NotFound();
            }

            //return View(absence);

            var model = new AbsenceEditViewModel
            {
                AbsenceId            = absence.Id,
                ReferenceNo           = absence.ReferenceNo,
                ReferenceDate         = absence.ReferenceDate,
                Subject               = absence.Subject,
                AbsenceReason        = absence.AbsenceReason,
                OnAbsenceDate        = absence.OnAbsenceDate,
                EnrollDate            = (DateTime)absence.EnrollDate,
                AbsenceInDays        = absence.AbsenceInDays,
                                                                                       
                Employees        = absence.Employees.Select(e => new EmployeesInfo12
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
         //   PopulateAbsenceList(model.AbsenceId);
            return View(model);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AbsenceId, ReferenceNo, ReferenceDate, Subject, AbsenceReason, AbsenceInDays,OnAbsenceDate, EnrollDate, Employees")] AbsenceEditViewModel model)
        {
            if (id != model.AbsenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var absence = await _context.Absences
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (absence == null)
                    {
                        return NotFound();
                    }

                    absence.ReferenceNo      = model.ReferenceNo;
                    absence.ReferenceDate    = model.ReferenceDate;
                    absence.Subject          = model.Subject;
                    absence.AbsenceReason    = model.AbsenceReason;
                    absence.OnAbsenceDate    = model.OnAbsenceDate;
                    absence.EnrollDate       = model.EnrollDate;
                    absence.AbsenceInDays = (model.EnrollDate - model.OnAbsenceDate).Days; //viewmodel.AbsenceInDays,
                 //   absence.AbsenceClassificationId = model.AbsenceClassificationId;

                    // Update the related employees
                    absence.Employees.Clear();
                    absence.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(absence);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _AbsenceRepo.Edit(absence);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _AbsenceRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("AbsencesEdit_POST", errMessage);
              //      PopulateAbsenceList(model.AbsenceId);
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
           //     PopulateAbsenceList(model.AbsenceId);

                return View(model);
            }
        }

        // GET: Absences/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _AbsenceRepo.GetItem((int)id);

            if (absence == null)
            {
                return NotFound();
            }
            
       //     PopulateAbsenceList(absence.AbsenceClassificationId);

            return View(absence);
        }

        // POST: Absences/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Absence absence)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string absenceInfo = "";// absence.ReferenceNo + " في " + absence.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _AbsenceRepo.Delete(absence);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _AbsenceRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("AbsencesDelete_POST", errMessage);
                return View(absence);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + absenceInfo + " بنجاح";
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
        //}

        //private void PopulateAbsenceList(object selectedAbsence = null)
        //{
        //    var AbsencesQuery = from l in _context.AbsenceClassifications
        //                       orderby l.Name
        //                       select l;
        //    ViewBag.AbsenceClassificationIds = new SelectList(AbsencesQuery.AsNoTracking(), "Id", "Name", selectedAbsence);

      }
    }

}
