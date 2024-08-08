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
    public class LocationClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ILocationClassification _LocationClassificationRepo;

        public LocationClassificationsController(NorthStormContext context, ILocationClassification locationClassificationRepo)
        {
            _context = context;
            _LocationClassificationRepo = locationClassificationRepo;
        }

        // GET: LocationClassifications
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

            PaginatedList<LocationClassification> items = await _LocationClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: LocationClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationClassification = await _LocationClassificationRepo.GetItem((int)id);

            if (locationClassification == null)
            {
                return NotFound();
            }

            return View(locationClassification);
        }

        // GET: LocationClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, KirkukSymbol, BaghdadSymbol")] LocationClassification locationClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _LocationClassificationRepo.Create(locationClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _LocationClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LocationClassificationsCreate_POST", errMessage);

                    return View(locationClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + locationClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(locationClassification);
            }
        }

        // GET: LocationClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationClassification = await _LocationClassificationRepo.GetItem((int)id);

            if (locationClassification == null)
            {
                return NotFound();
            }

            return View(locationClassification);
        }

        // POST: LocationClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, KirkukSymbol, BaghdadSymbol")] LocationClassification locationClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != locationClassification.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _LocationClassificationRepo.Edit(locationClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _LocationClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LocationClassificationsEdit_POST", errMessage);

                    return View(locationClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + locationClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(locationClassification);
            }
        }

        // GET: LocationClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationClassification = await _LocationClassificationRepo.GetItem((int)id);

            if (locationClassification == null)
            {
                return NotFound();
            }

            return View(locationClassification);
        }

        // POST: LocationClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(LocationClassification locationClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string locationClassificationInfo = locationClassification.Name;

            try
            {
                IsDeleted = await _LocationClassificationRepo.Delete(locationClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _LocationClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("LocationClassificationsDelete_POST", errMessage);
                return View(locationClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + locationClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}