using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

                                            // Dismiss Types Controller

namespace NorthStorm.Controllers.Classifications
{
     public class DismissClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IDismissClassification _DismissClassificationRepo;

        public DismissClassificationsController(NorthStormContext context, IDismissClassification dismissClassificationRepo)
        {
            _context = context;
            _DismissClassificationRepo = dismissClassificationRepo;
        }

        // GET: DismissClassifications
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

            PaginatedList<DismissClassification> items = await _DismissClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: DismissClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dismissClassification = await _DismissClassificationRepo.GetItem((int)id);

            if (dismissClassification == null)
            {
                return NotFound();
            }

            return View(dismissClassification);
        }

        // GET: DismissClassifications/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }
        // POST: DismissClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, StatusId, DismissSPSSCode")] DismissClassification dismissClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _DismissClassificationRepo.Create(dismissClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _DismissClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("DismissClassificationsCreate_POST", errMessage);
                    return View(dismissClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة طرح الكادر " + dismissClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(dismissClassification.StatusId);
                return View(dismissClassification);
            }
        }
        // GET: DismissClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dismissClassification = await _DismissClassificationRepo.GetItem((int)id);
            if (dismissClassification == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(dismissClassification.StatusId);
            return View(dismissClassification);
        }

        // POST: DismissClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, StatusId, DismissSPSSCode")] DismissClassification dismissClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != dismissClassification.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _DismissClassificationRepo.Edit(dismissClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }
                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _DismissClassificationRepo.GetErrors();
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("DismissClassificationsEdit_POST", errMessage);
                    PopulateDropDownLists(dismissClassification.StatusId);
                    return View(dismissClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث اشغال الملاك " + dismissClassification.Name + " بنجاح";
               
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";
                PopulateDropDownLists(dismissClassification.StatusId);
                return View(dismissClassification);
            }
        }

        // GET: DismissClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dismissClassification = await _DismissClassificationRepo.GetItem((int)id);

            if (dismissClassification == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(dismissClassification.StatusId);
            return View(dismissClassification);
        }

        // POST: DismissClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DismissClassification dismissClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string dismissClassificationInfo = dismissClassification.Name;

            try
            {
                IsDeleted = await _DismissClassificationRepo.Delete(dismissClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _DismissClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("DismissClassificationsDelete_POST", errMessage);
                return View(dismissClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + dismissClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropDownLists(
         object selectedStatus = null)
        {
            var statusesQuery = from g in _context.Statuses
                               orderby g.Name
                               select g;
            ViewBag.StatusId = new SelectList(statusesQuery.AsNoTracking(), "Id", "Name", selectedStatus);
        }
    }
}

