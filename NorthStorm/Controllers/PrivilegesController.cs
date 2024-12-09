using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Privileges Controller
namespace NorthStorm.Controllers
{
    public class PrivilegesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IPrivilege _PrivilegeRepo;

        public PrivilegesController(NorthStormContext context, IPrivilege privilegeRepo)
        {
            _context = context;
            _PrivilegeRepo = privilegeRepo;
        }
        // GET: Privileges
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
                ViewData["PrivilegeId"] = selectedId.Value;
            ViewBag.SearchText = SearchText;
            PaginatedList<Privilege> items = await _PrivilegeRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;
            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Privileges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var privilege = await _PrivilegeRepo.GetItem((int)id);
            if (privilege == null)
            {
                return NotFound();
            }
            return View(privilege);
        }
       // GET: Privilege/CreateMasterDetails
        public IActionResult Create()
        {
            PopulatePrivilegeList();
            var item = new PrivilegeCreateViewModel();
            return View(item);
        }
        // POST: Privilege/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNo, ReferenceDate, Subject, PrivilegeClassificationId, PrivilegeId, EmployeeIds")] PrivilegeCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";
                var privilege = new Privilege
                {
                    ReferenceNo              = viewmodel.ReferenceNo,
                    ReferenceDate            = viewmodel.ReferenceDate,
                    Subject                  = viewmodel.Subject,
                   
                    PrivilegeClassificationId    = viewmodel.PrivilegeClassificationId,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };
                try
                {
                    IsCreated = await _PrivilegeRepo.Create(privilege);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _PrivilegeRepo.GetErrors();
                    PopulatePrivilegeList(privilege.PrivilegeClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PrivilegesCreate_POST", errMessage);
                    return View(privilege);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + privilege.ReferenceNo + " المؤرخ في " + privilege.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulatePrivilegeList(viewmodel.PrivilegeClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }
        // GET: Privileges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var privilege = await _PrivilegeRepo.GetItem((int)id);
            if (privilege == null)
            {
                return NotFound();
            }

            //return View(privilege);
            var model = new PrivilegeEditViewModel
            {
                PrivilegeId            = privilege.Id,
                ReferenceNo           = privilege.ReferenceNo,
                ReferenceDate         = privilege.ReferenceDate,
                Subject               = privilege.Subject,
               
                PrivilegeClassificationId = privilege.PrivilegeClassificationId, 
                Employees        = privilege.Employees.Select(e => new EmployeesInfo8
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
            PopulatePrivilegeList(model.PrivilegeId);
            return View(model);
        }
        // POST: Privileges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrivilegeId, ReferenceNo, ReferenceDate, Subject, PrivilegeClassificationId, Employees")] PrivilegeEditViewModel model)
        {
            if (id != model.PrivilegeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var privilege = await _context.Privileges
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);
                    if (privilege == null)
                    {
                        return NotFound();
                    }
                    privilege.ReferenceNo           = model.ReferenceNo;
                    privilege.ReferenceDate         = model.ReferenceDate;
                    privilege.Subject               = model.Subject;
                    privilege.PrivilegeClassificationId = model.PrivilegeClassificationId;
                    // Update the related employees
                    privilege.Employees.Clear();
                    privilege.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(privilege);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _PrivilegeRepo.Edit(privilege);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _PrivilegeRepo.GetErrors();
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PrivilegesEdit_POST", errMessage);
                    PopulatePrivilegeList(model.PrivilegeId);
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
                PopulatePrivilegeList(model.PrivilegeId);
                return View(model);
            }
        }
        // GET: Privileges/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var privilege = await _PrivilegeRepo.GetItem((int)id);
            if (privilege == null)
            {
                return NotFound();
            }
            PopulatePrivilegeList(privilege.PrivilegeClassificationId);
            return View(privilege);
        }

        // POST: Privileges/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Privilege privilege)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string privilegeInfo = "";// privilege.ReferenceNo + " في " + privilege.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _PrivilegeRepo.Delete(privilege);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _PrivilegeRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("PrivilegesDelete_POST", errMessage);
                return View(privilege);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + privilegeInfo + " بنجاح";
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
        private void PopulatePrivilegeList(object selectedPrivilege = null)
        {
            var PrivilegesQuery = from l in _context.PrivilegeClassifications
                               orderby l.Name
                               select l;
            ViewBag.PrivilegeClassificationIds = new SelectList(PrivilegesQuery.AsNoTracking(), "Id", "Name", selectedPrivilege);

        }
    }

}
