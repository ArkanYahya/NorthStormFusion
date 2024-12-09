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

                               // ThankClassification Controller


namespace NorthStorm.Controllers.Classifications
{
    public class ThankClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IThankClassification _ThankClassificationRepo;

        public ThankClassificationsController(NorthStormContext context, IThankClassification thankClassificationRepo)
        {
            _context = context;
            _ThankClassificationRepo = thankClassificationRepo;
        }

        // GET: ThankClassifications
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

            PaginatedList<ThankClassification> items = await _ThankClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: ThankClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankClassification = await _ThankClassificationRepo.GetItem((int)id);

            if (thankClassification == null)
            {
                return NotFound();
            }

            return View(thankClassification);
        }

        // GET: ThankClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThankClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, ThankSeniority")] ThankClassification thankClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _ThankClassificationRepo.Create(thankClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ThankClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ThankClassificationsCreate_POST", errMessage);

                    return View(thankClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الشكر " + thankClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(thankClassification);
            }
        }

        // GET: ThankClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankClassification = await _ThankClassificationRepo.GetItem((int)id);

            if (thankClassification == null)
            {
                return NotFound();
            }

            return View(thankClassification);
        }

        // POST: ThankClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ThankSeniority")] ThankClassification thankClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != thankClassification.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _ThankClassificationRepo.Edit(thankClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ThankClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ThankClassificationsEdit_POST", errMessage);
                    return View(thankClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الشكر " + thankClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(thankClassification);
            }
        }

        // GET: ThankClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thankClassification = await _ThankClassificationRepo.GetItem((int)id);

            if (thankClassification == null)
            {
                return NotFound();
            }

            return View(thankClassification);
        }

        // POST: ThankClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ThankClassification thankClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string thankClassificationInfo = thankClassification.Name;

            try
            {
                IsDeleted = await _ThankClassificationRepo.Delete(thankClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ThankClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ThankClassificationsDelete_POST", errMessage);
                return View(thankClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + thankClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}