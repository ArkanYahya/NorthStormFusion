using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;


                     // PrivilegeClassification Controller


namespace NorthStorm.Controllers
{
    public class PrivilegeClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IPrivilegeClassification _PrivilegeClassificationRepo;

        public PrivilegeClassificationsController(NorthStormContext context, IPrivilegeClassification privilegeClassificationRepo)
        {
            _context = context;
            _PrivilegeClassificationRepo = privilegeClassificationRepo;
        }

        // GET: PrivilegeClassifications
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 20)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("PrivilegeSeverity");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<PrivilegeClassification> items = await _PrivilegeClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: PrivilegeClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeClassification = await _PrivilegeClassificationRepo.GetItem((int)id);

            if (privilegeClassification == null)
            {
                return NotFound();
            }

            return View(privilegeClassification);
        }

        // GET: PrivilegeClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrivilegeClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, ExtendingRetirementAge, PromotionBenefites, OtherBenefites")] PrivilegeClassification privilegeClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _PrivilegeClassificationRepo.Create(privilegeClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _PrivilegeClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PrivilegeClassificationsCreate_POST", errMessage);

                    return View(privilegeClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الامتياز " + privilegeClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(privilegeClassification);
            }
        }

        // GET: PrivilegeClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeClassification = await _PrivilegeClassificationRepo.GetItem((int)id);

            if (privilegeClassification == null)
            {
                return NotFound();
            }

            return View(privilegeClassification);
        }

        // POST: PrivilegeClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name,  ExtendingRetirementAge, PromotionBenefites, OtherBenefites")] PrivilegeClassification privilegeClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != privilegeClassification.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _PrivilegeClassificationRepo.Edit(privilegeClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _PrivilegeClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PrivilegeClassificationsEdit_POST", errMessage);
                    return View(privilegeClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الامتياز " + privilegeClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(privilegeClassification);
            }
        }

        // GET: PrivilegeClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeClassification = await _PrivilegeClassificationRepo.GetItem((int)id);

            if (privilegeClassification == null)
            {
                return NotFound();
            }

            return View(privilegeClassification);
        }

        // POST: PrivilegeClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(PrivilegeClassification privilegeClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string privilegeClassificationInfo = privilegeClassification.Name;

            try
            {
                IsDeleted = await _PrivilegeClassificationRepo.Delete(privilegeClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _PrivilegeClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("PrivilegeClassificationsDelete_POST", errMessage);
                return View(privilegeClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + privilegeClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}