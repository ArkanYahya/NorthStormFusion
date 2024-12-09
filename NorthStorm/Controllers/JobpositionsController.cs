using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // JobPositions Controller
namespace NorthStorm.Controllers
{
    public class JobPositionsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IJobPosition _JobPositionRepo;

        public JobPositionsController(NorthStormContext context, IJobPosition jobPositionRepo)
        {
            _context = context;
            _JobPositionRepo = jobPositionRepo;
        }
        // GET: JobPositions
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
                ViewData["JobPositionId"] = selectedId.Value;
            ViewBag.SearchText = SearchText;
            PaginatedList<JobPosition> items = await _JobPositionRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: JobPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobPosition = await _JobPositionRepo.GetItem((int)id);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }
       // GET: JobPosition/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateJobPositionList();
            var item = new JobPositionCreateViewModel();
            return View(item);
        }
        // POST: JobPosition/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, ResponsibleClassificationId, DeputyClassificationId, JobPositionId, StartingFromDate, EmployeeIds")] JobPositionCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";
                var jobPosition = new JobPosition
                {
                    ReferenceNo              = viewmodel.ReferenceNo,
                    ReferenceDate            = viewmodel.ReferenceDate,
                    Subject                  = viewmodel.Subject,
                    StartingFromDate         = viewmodel.StartingFromDate,
                    ResponsibleClassificationId  = viewmodel.ResponsibleClassificationId,
                    DeputyClassificationId       = viewmodel.DeputyClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };
                try
                {
                    IsCreated = await _JobPositionRepo.Create(jobPosition);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _JobPositionRepo.GetErrors();
                    PopulateJobPositionList(jobPosition.ResponsibleClassificationId,
                                            jobPosition.DeputyClassificationId);


                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobPositionsCreate_POST", errMessage);
                    return View(jobPosition);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + jobPosition.ReferenceNo + " المؤرخ في " + jobPosition.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateJobPositionList(viewmodel.ResponsibleClassificationId,
                                        viewmodel.DeputyClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }
        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobPosition = await _JobPositionRepo.GetItem((int)id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            //return View(jobPosition);
            var model = new JobPositionEditViewModel
            {
                JobPositionId            = jobPosition.Id,
                ReferenceNo           = jobPosition.ReferenceNo,
                ReferenceDate         = jobPosition.ReferenceDate,
                Subject               = jobPosition.Subject,
                StartingFromDate = (DateTime)jobPosition.StartingFromDate,
                ResponsibleClassificationId = jobPosition.ResponsibleClassificationId,
                DeputyClassificationId = jobPosition.DeputyClassificationId,
                Employees        = jobPosition.Employees.Select(e => new EmployeesInfo10
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateJobPositionList(model.JobPositionId,
                model.ResponsibleClassificationId,
                model.DeputyClassificationId);
            return View(model);
        }
        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobPositionId, ReferenceNo, ReferenceDate, Subject, ResponsibleClassificationId, DeputyClassificationId, StartingFromDate, Employees")] JobPositionEditViewModel model)
        {
            if (id != model.JobPositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var jobPosition = await _context.JobPositions
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);
                    if (jobPosition == null)
                    {
                        return NotFound();
                    }
                    jobPosition.ReferenceNo           = model.ReferenceNo;
                    jobPosition.ReferenceDate         = model.ReferenceDate;
                    jobPosition.Subject               = model.Subject;
                    jobPosition.StartingFromDate = model.StartingFromDate;
                    jobPosition.ResponsibleClassificationId = model.ResponsibleClassificationId;
                    jobPosition.DeputyClassificationId = model.DeputyClassificationId;
                    // Update the related employees
                    jobPosition.Employees.Clear();
                    jobPosition.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(jobPosition);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _JobPositionRepo.Edit(jobPosition);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _JobPositionRepo.GetErrors();
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobPositionsEdit_POST", errMessage);
                    PopulateJobPositionList(model.JobPositionId,
                        model.ResponsibleClassificationId,
                        model.DeputyClassificationId);
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
                PopulateJobPositionList(model.JobPositionId,
                    model.ResponsibleClassificationId,
                        model.DeputyClassificationId);
                return View(model);
            }
        }
        // GET: JobPositions/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobPosition = await _JobPositionRepo.GetItem((int)id);
            if (jobPosition == null)
            {
                return NotFound();
            }
            PopulateJobPositionList(jobPosition.ResponsibleClassificationId, 
                jobPosition.DeputyClassificationId);
            return View(jobPosition);
        }

        // POST: JobPositions/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(JobPosition jobPosition)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string jobPositionInfo = "";// jobPosition.ReferenceNo + " في " + jobPosition.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _JobPositionRepo.Delete(jobPosition);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _JobPositionRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("JobPositionsDelete_POST", errMessage);
                return View(jobPosition);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + jobPositionInfo + " بنجاح";
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
        private void PopulateJobPositionList(object selectedJobPosition = null, int? deputyClassificationId = null, int? deputyClassificationId1 = null)
        {
            var JobPositionsQuery = from l in _context.ResponsibleClassifications
                                    orderby l.Name
                                    select l;
            ViewBag.ResponsibleClassificationIds = new SelectList(JobPositionsQuery.AsNoTracking(), "Id", "Name", selectedJobPosition);

       
            var DeputyClassificationQuery = from l in _context.DeputyClassifications
                                    orderby l.Name
                                    select l;
            object selectedDeputyClassificationsId = null;
            ViewBag.DeputyClassificationsIds = new SelectList(DeputyClassificationQuery.AsNoTracking(), "Id", "Name", selectedDeputyClassificationsId);

    }


}

}
