using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Controllers
{
    public class JobTransfersController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IJobTransfer _JobTransferRepo;

        public JobTransfersController(NorthStormContext context, IJobTransfer jobTransferRepo)
        {
            _context = context;
            _JobTransferRepo = jobTransferRepo;
        }

        // GET: JobTransfers
        public async Task<IActionResult> Index(
            int? selectedId,
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("ReferenceNo");
            sortModel.AddColumn("ReferenceDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            if (selectedId != null)
                ViewData["JobTransferId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<JobTransfer> items = await _JobTransferRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: JobTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTransfer = await _JobTransferRepo.GetItem((int)id);

            if (jobTransfer == null)
            {
                return NotFound();
            }

            return View(jobTransfer);
        }


        // GET: JobTransfer/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateLevelList();
            var item = new JobTransferCreateViewModel();
            return View(item);
        }

        // POST: JobTransfer/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, DestinationLevelId, EmployeeIds")] JobTransferCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var jobTransfer = new JobTransfer
                {
                    ReferenceNo = model.ReferenceNo,
                    ReferenceDate = model.ReferenceDate,
                    Subject = model.Subject,
                    DestinationLevelId = model.DestinationLevelId,
                    Employees = await _context.Employees
                                             .Where(e => model.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _JobTransferRepo.Create(jobTransfer);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _JobTransferRepo.GetErrors();

                    PopulateLevelList(jobTransfer.DestinationLevelId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTransfersCreate_POST", errMessage);

                    return View(jobTransfer);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + jobTransfer.ReferenceNo + " المؤرخ في " + jobTransfer.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateLevelList(model.DestinationLevelId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(model);
            }
        }


        // GET: JobTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTransfer = await _JobTransferRepo.GetItem((int)id);

            if (jobTransfer == null)
            {
                return NotFound();
            }

            //return View(jobTransfer);

            var model = new JobTransferEditViewModel
            {
                JobTransferId = jobTransfer.Id,
                ReferenceNo = jobTransfer.ReferenceNo,
                ReferenceDate = jobTransfer.ReferenceDate,
                Subject = jobTransfer.Subject,
                DestinationLevelId = jobTransfer.DestinationLevelId,
                Employees = jobTransfer.Employees.Select(e => new EmployeesInfo
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateLevelList(model.DestinationLevelId);
            return View(model);
        }

        // POST: JobTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobTransferId,ReferenceNo,ReferenceDate,Subject, DestinationLevelId, Employees")] JobTransferEditViewModel model)
        {
            if (id != model.JobTransferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var jobTransfer = await _context.JobTransfers
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (jobTransfer == null)
                    {
                        return NotFound();
                    }

                    jobTransfer.ReferenceNo = model.ReferenceNo;
                    jobTransfer.ReferenceDate = model.ReferenceDate;
                    jobTransfer.Subject = model.Subject;
                    jobTransfer.DestinationLevelId = model.DestinationLevelId;

                    // Update the related employees
                    jobTransfer.Employees.Clear();
                    jobTransfer.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(jobTransfer);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _JobTransferRepo.Edit(jobTransfer);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _JobTransferRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTransfersEdit_POST", errMessage);
                    PopulateLevelList(model.DestinationLevelId);
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
                PopulateLevelList(model.DestinationLevelId);

                return View(model);
            }
        }

        // GET: JobTransfers/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTransfer = await _JobTransferRepo.GetItem((int)id);

            if (jobTransfer == null)
            {
                return NotFound();
            }
            
            PopulateLevelList(jobTransfer.DestinationLevelId);

            return View(jobTransfer);
        }

        // POST: JobTransfers/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(JobTransfer jobTransfer)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string jobTransferInfo = "";// jobTransfer.ReferenceNo + " في " + jobTransfer.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _JobTransferRepo.Delete(jobTransfer);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _JobTransferRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("JobTransfersDelete_POST", errMessage);
                return View(jobTransfer);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + jobTransferInfo + " بنجاح";
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

        private void PopulateLevelList(object selectedLevel = null)
        {
            var levelsQuery = from l in _context.Levels
                               orderby l.Name
                               select l;
            ViewBag.LevelId = new SelectList(levelsQuery.AsNoTracking(), "Id", "Name", selectedLevel);

        }
    }

}
