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


                     // Marital Status Classifications Controller


namespace NorthStorm.Controllers
{
    public class MaritalStatusClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IMaritalStatusClassification _MaritalStatusClassificationRepo;

        public MaritalStatusClassificationsController(NorthStormContext context, IMaritalStatusClassification maritalStatusClassificationRepo)
        {
            _context = context;
            _MaritalStatusClassificationRepo = maritalStatusClassificationRepo;
        }

        // GET: MaritalStatusClassifications
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 20)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
      //      sortModel.AddColumn("MaritalStatusSeverity");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<MaritalStatusClassification> items = await _MaritalStatusClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: MaritalStatusClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusClassification = await _MaritalStatusClassificationRepo.GetItem((int)id);

            if (maritalStatusClassification == null)
            {
                return NotFound();
            }

            return View(maritalStatusClassification);
        }

        // GET: MaritalStatusClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaritalStatusClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,  MaritalStatusAllowance, MaritalStatusSPSS")] MaritalStatusClassification maritalStatusClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _MaritalStatusClassificationRepo.Create(maritalStatusClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _MaritalStatusClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("MaritalStatusClassificationsCreate_POST", errMessage);

                    return View(maritalStatusClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الحالة الاجتماعية " + maritalStatusClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(maritalStatusClassification);
            }
        }

        // GET: MaritalStatusClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusClassification = await _MaritalStatusClassificationRepo.GetItem((int)id);

            if (maritalStatusClassification == null)
            {
                return NotFound();
            }

            return View(maritalStatusClassification);
        }

        // POST: MaritalStatusClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, MaritalStatusAllowance, MaritalStatusSPSS")] MaritalStatusClassification maritalStatusClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != maritalStatusClassification.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _MaritalStatusClassificationRepo.Edit(maritalStatusClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _MaritalStatusClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("MaritalStatusClassificationsEdit_POST", errMessage);
                    return View(maritalStatusClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الحالة الاجتماعية " + maritalStatusClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(maritalStatusClassification);
            }
        }

        // GET: MaritalStatusClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritalStatusClassification = await _MaritalStatusClassificationRepo.GetItem((int)id);

            if (maritalStatusClassification == null)
            {
                return NotFound();
            }

            return View(maritalStatusClassification);
        }

        // POST: MaritalStatusClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(MaritalStatusClassification maritalStatusClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string maritalStatusClassificationInfo = maritalStatusClassification.Name;

            try
            {
                IsDeleted = await _MaritalStatusClassificationRepo.Delete(maritalStatusClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _MaritalStatusClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("MaritalStatusClassificationsDelete_POST", errMessage);
                return View(maritalStatusClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + maritalStatusClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}