using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Controllers
{
    public class ThankAndAppreciationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IThankAndAppreciation _ThankAndAppreciationRepo;

        public ThankAndAppreciationsController(NorthStormContext context, IThankAndAppreciation thankAndAppreciationRepo)
        {
            _context = context;
            _ThankAndAppreciationRepo = thankAndAppreciationRepo;
        }

        // GET: ThankAndAppreciations
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
                ViewData["ThankAndAppreciationId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<ThankAndAppreciation> items = await _ThankAndAppreciationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: ThankAndAppreciations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankAndAppreciation = await _ThankAndAppreciationRepo.GetItem((int)id);

            if (thankAndAppreciation == null)
            {
                return NotFound();
            }

            return View(thankAndAppreciation);
        }


        // GET: ThankAndAppreciation/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateThankAndAppreciationList();
            var item = new ThankAndAppreciationCreateViewModel();
            return View(item);
        }

        // POST: ThankAndAppreciation/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, ThankAndAppreciationReason, ThankClassification, ThankAndAppreciationId,ThankSeniority, EmployeeIds")] ThankAndAppreciationCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var thankAndAppreciation = new ThankAndAppreciation
                {
                    ReferenceNo                 = viewmodel.ReferenceNo,
                    ReferenceDate               = viewmodel.ReferenceDate,
                    Subject                     = viewmodel.Subject,
                    ThankAndAppreciationReason  = viewmodel.ThankAndAppreciationReason,
                    ThankClassification         = viewmodel.ThankClassification,
                    ThankClassificationId       = viewmodel.ThankClassificationId,
                    ThankSeniority              = viewmodel.ThankSeniority,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _ThankAndAppreciationRepo.Create(thankAndAppreciation);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ThankAndAppreciationRepo.GetErrors();

                    PopulateThankAndAppreciationList(thankAndAppreciation.ThankClassification);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ThankAndAppreciationsCreate_POST", errMessage);

                    return View(thankAndAppreciation);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + thankAndAppreciation.ReferenceNo + " المؤرخ في " + thankAndAppreciation.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateThankAndAppreciationList(viewmodel.ThankClassification);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: ThankAndAppreciations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankAndAppreciation = await _ThankAndAppreciationRepo.GetItem((int)id);

            if (thankAndAppreciation == null)
            {
                return NotFound();
            }

            //return View(thankAndAppreciation);

            var model = new ThankAndAppreciationEditViewModel
            {
                ThankAndAppreciationId     = thankAndAppreciation.Id,
                ReferenceNo                = thankAndAppreciation.ReferenceNo,
                ReferenceDate              = thankAndAppreciation.ReferenceDate,
                Subject                    = thankAndAppreciation.Subject,
                ThankAndAppreciationReason = thankAndAppreciation.ThankAndAppreciationReason,
                ThankClassificationId      = thankAndAppreciation.ThankClassificationId,
                ThankSeniority             = thankAndAppreciation.ThankSeniority,
                Employees        = thankAndAppreciation.Employees.Select(e => new EmployeesInfo5
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulateThankAndAppreciationList(model.ThankAndAppreciationId);
            return View(model);
        }

        // POST: ThankAndAppreciations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThankAndAppreciationId,ReferenceNo, ReferenceDate, Subject, ThankAndAppreciationReason, ThankClassification, ThankAndAppreciationId,ThankSeniority")] ThankAndAppreciationEditViewModel model)
        {
            if (id != model.ThankAndAppreciationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var thankAndAppreciation = await _context.ThankAndAppreciations
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (thankAndAppreciation == null)
                    {
                        return NotFound();
                    }

                    thankAndAppreciation.ReferenceNo      = model.ReferenceNo;
                    thankAndAppreciation.ReferenceDate    = model.ReferenceDate;
                    thankAndAppreciation.Subject          = model.Subject;
                    thankAndAppreciation.ThankAndAppreciationReason = model.ThankAndAppreciationReason;
                    thankAndAppreciation.ThankClassification = model.ThankClassification;
                    thankAndAppreciation.ThankClassificationId = model.ThankClassificationId;
                    thankAndAppreciation.ThankSeniority = model.ThankSeniority;

                    // Update the related employees
                    thankAndAppreciation.Employees.Clear();
                    thankAndAppreciation.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(thankAndAppreciation);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _ThankAndAppreciationRepo.Edit(thankAndAppreciation);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ThankAndAppreciationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ThankAndAppreciationsEdit_POST", errMessage);
                    PopulateThankAndAppreciationList(model.ThankAndAppreciationId);
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
                PopulateThankAndAppreciationList(model.ThankAndAppreciationId);

                return View(model);
            }
        }

        // GET: ThankAndAppreciations/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankAndAppreciation = await _ThankAndAppreciationRepo.GetItem((int)id);

            if (thankAndAppreciation == null)
            {
                return NotFound();
            }
            
            PopulateThankAndAppreciationList(thankAndAppreciation.ThankClassification);

            return View(thankAndAppreciation);
        }

        // POST: ThankAndAppreciations/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ThankAndAppreciation thankAndAppreciation)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string thankAndAppreciationInfo = "";// thankAndAppreciation.ReferenceNo + " في " + thankAndAppreciation.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _ThankAndAppreciationRepo.Delete(thankAndAppreciation);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ThankAndAppreciationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ThankAndAppreciationsDelete_POST", errMessage);
                return View(thankAndAppreciation);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + thankAndAppreciationInfo + " بنجاح";
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

        private void PopulateThankAndAppreciationList(object selectedThankAndAppreciation = null)
        {
            var ThankAndAppreciationsQuery = from l in _context.ThankClassifications
                               orderby l.Name
                               select l;
            ViewBag.ThankClassifications = new SelectList(ThankAndAppreciationsQuery.AsNoTracking(), "Id", "Name", selectedThankAndAppreciation);

        }
    }

}
