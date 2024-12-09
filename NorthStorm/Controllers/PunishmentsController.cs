using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Controllers
{
    public class PunishmentsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IPunishment _PunishmentRepo;

        public PunishmentsController(NorthStormContext context, IPunishment punishmentRepo)
        {
            _context = context;
            _PunishmentRepo = punishmentRepo;
        }

        // GET: Punishments
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
                ViewData["PunishmentId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Punishment> items = await _PunishmentRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Punishments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _PunishmentRepo.GetItem((int)id);

            if (punishment == null)
            {
                return NotFound();
            }

            return View(punishment);
        }


        // GET: Punishment/CreateMasterDetails
        public IActionResult Create()
        {
            PopulatePunishmentList();
            var item = new PunishmentCreateViewModel();
            return View(item);
        }

        // POST: Punishment/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, PunishmentReason, PunishmentTypeId, PunishmentId, EmployeeIds")] PunishmentCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var punishment = new Punishment
                {
                    ReferenceNo       = viewmodel.ReferenceNo,
                    ReferenceDate     = viewmodel.ReferenceDate,
                    Subject           = viewmodel.Subject,
                    PunishmentReason  = viewmodel.PunishmentReason,
                    PunishmentTypeId  = viewmodel.PunishmentTypeId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _PunishmentRepo.Create(punishment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _PunishmentRepo.GetErrors();

                    PopulatePunishmentList(punishment.PunishmentTypeId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PunishmentsCreate_POST", errMessage);

                    return View(punishment);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + punishment.ReferenceNo + " المؤرخ في " + punishment.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulatePunishmentList(viewmodel.PunishmentTypeId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: Punishments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _PunishmentRepo.GetItem((int)id);

            if (punishment == null)
            {
                return NotFound();
            }

            //return View(punishment);

            var model = new PunishmentEditViewModel
            {
                PunishmentId     = punishment.Id,
                ReferenceNo      = punishment.ReferenceNo,
                ReferenceDate    = punishment.ReferenceDate,
                Subject          = punishment.Subject,
                PunishmentReason = punishment.PunishmentReason,
                PunishmentTypeId = punishment.PunishmentTypeId, 
                Employees        = punishment.Employees.Select(e => new EmployeesInfo2
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulatePunishmentList(model.PunishmentId);
            return View(model);
        }

        // POST: Punishments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PunishmentId,ReferenceNo,ReferenceDate,Subject,PunishmentReason, PunishmentTypeId, Employees")] PunishmentEditViewModel model)
        {
            if (id != model.PunishmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var punishment = await _context.Punishments
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (punishment == null)
                    {
                        return NotFound();
                    }

                    punishment.ReferenceNo      = model.ReferenceNo;
                    punishment.ReferenceDate    = model.ReferenceDate;
                    punishment.Subject          = model.Subject;
                    punishment.PunishmentReason = model.PunishmentReason;
                    punishment.PunishmentTypeId = model.PunishmentTypeId;

                    // Update the related employees
                    punishment.Employees.Clear();
                    punishment.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(punishment);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _PunishmentRepo.Edit(punishment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _PunishmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PunishmentsEdit_POST", errMessage);
                    PopulatePunishmentList(model.PunishmentId);
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
                PopulatePunishmentList(model.PunishmentId);

                return View(model);
            }
        }

        // GET: Punishments/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _PunishmentRepo.GetItem((int)id);

            if (punishment == null)
            {
                return NotFound();
            }
            
            PopulatePunishmentList(punishment.PunishmentTypeId);

            return View(punishment);
        }

        // POST: Punishments/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Punishment punishment)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string punishmentInfo = "";// punishment.ReferenceNo + " في " + punishment.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _PunishmentRepo.Delete(punishment);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _PunishmentRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("PunishmentsDelete_POST", errMessage);
                return View(punishment);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + punishmentInfo + " بنجاح";
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

        private void PopulatePunishmentList(object selectedPunishment = null)
        {
            var PunishmentsQuery = from l in _context.PunishmentTypes
                               orderby l.Name
                               select l;
            ViewBag.PunishmentTypeIds = new SelectList(PunishmentsQuery.AsNoTracking(), "Id", "Name", selectedPunishment);

        }
    }

}
