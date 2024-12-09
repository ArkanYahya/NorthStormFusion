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

namespace NorthStorm.Controllers.Classifications
{
                                    // ShiftClassification Controller
    public class ShiftClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IShiftClassification _ShiftClassificationRepo;

        public ShiftClassificationsController(NorthStormContext context, IShiftClassification itemRepo)
        {
            _context = context;
            _ShiftClassificationRepo = itemRepo;
        }

        // GET: ShiftClassifications
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<ShiftClassification> items = await _ShiftClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: ShiftClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _ShiftClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: ShiftClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, KirkukSymbol, BaghdadSymbol")] ShiftClassification item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _ShiftClassificationRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ShiftClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ShiftClassificationsCreate_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + item.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(item);
            }
        }

        // GET: ShiftClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _ShiftClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ShiftClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, KirkukSymbol, BaghdadSymbol")] ShiftClassification item)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != item.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _ShiftClassificationRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ShiftClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ShiftClassificationsEdit_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + item.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(item);
            }
        }

        // GET: ShiftClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _ShiftClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ShiftClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ShiftClassification item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _ShiftClassificationRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ShiftClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ShiftClassificationsDelete_POST", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + itemInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}