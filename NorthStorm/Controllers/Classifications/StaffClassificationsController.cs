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

                                            // StaffClassification Controller

namespace NorthStorm.Controllers.Classifications
{
     public class StaffClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IStaffClassification _StaffClassificationRepo;

        public StaffClassificationsController(NorthStormContext context, IStaffClassification staffClassificationRepo)
        {
            _context = context;
            _StaffClassificationRepo = staffClassificationRepo;
        }

        // GET: StaffClassifications
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<StaffClassification> items = await _StaffClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: StaffClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffClassification = await _StaffClassificationRepo.GetItem((int)id);

            if (staffClassification == null)
            {
                return NotFound();
            }

            return View(staffClassification);
        }

        // GET: StaffClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, StaffPeriod")] StaffClassification staffClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _StaffClassificationRepo.Create(staffClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _StaffClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StaffClassificationsCreate_POST", errMessage);

                    return View(staffClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة نوع الملاك " + staffClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(staffClassification);
            }
        }

        // GET: StaffClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffClassification = await _StaffClassificationRepo.GetItem((int)id);

            if (staffClassification == null)
            {
                return NotFound();
            }

            return View(staffClassification);
        }

        // POST: StaffClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, StaffPeriod")] StaffClassification staffClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != staffClassification.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _StaffClassificationRepo.Edit(staffClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _StaffClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StaffClassificationsEdit_POST", errMessage);
                    return View(staffClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث نوع الملاك " + staffClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(staffClassification);
            }
        }

        // GET: StaffClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffClassification = await _StaffClassificationRepo.GetItem((int)id);

            if (staffClassification == null)
            {
                return NotFound();
            }

            return View(staffClassification);
        }

        // POST: StaffClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StaffClassification staffClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string staffClassificationInfo = staffClassification.Name;

            try
            {
                IsDeleted = await _StaffClassificationRepo.Delete(staffClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _StaffClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("StaffClassificationsDelete_POST", errMessage);
                return View(staffClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + staffClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}