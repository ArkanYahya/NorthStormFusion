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



                     // Comptence Classification Controller

namespace NorthStorm.Controllers.Classifications
{
    public class ComptenceClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IComptenceClassification _ComptenceClassificationRepo;

        public ComptenceClassificationsController(NorthStormContext context, IComptenceClassification comptenceClassificationRepo)
        {
            _context = context;
            _ComptenceClassificationRepo = comptenceClassificationRepo;
        }

        // GET: ComptenceClassifications
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

            PaginatedList<ComptenceClassification> items = await _ComptenceClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: ComptenceClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptenceClassification = await _ComptenceClassificationRepo.GetItem((int)id);

            if (comptenceClassification == null)
            {
                return NotFound();
            }

            return View(comptenceClassification);
        }

        // GET: ComptenceClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComptenceClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, KirkukSymbol, BaghdadSymbol")] ComptenceClassification comptenceClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _ComptenceClassificationRepo.Create(comptenceClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ComptenceClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ComptenceClassificationsCreate_POST", errMessage);

                    return View(comptenceClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + comptenceClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(comptenceClassification);
            }
        }

        // GET: ComptenceClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptenceClassification = await _ComptenceClassificationRepo.GetItem((int)id);

            if (comptenceClassification == null)
            {
                return NotFound();
            }

            return View(comptenceClassification);
        }

        // POST: ComptenceClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, KirkukSymbol, BaghdadSymbol")] ComptenceClassification comptenceClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != comptenceClassification.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _ComptenceClassificationRepo.Edit(comptenceClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ComptenceClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ComptenceClassificationsEdit_POST", errMessage);

                    return View(comptenceClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + comptenceClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(comptenceClassification);
            }
        }

        // GET: ComptenceClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptenceClassification = await _ComptenceClassificationRepo.GetItem((int)id);

            if (comptenceClassification == null)
            {
                return NotFound();
            }

            return View(comptenceClassification);
        }

        // POST: ComptenceClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ComptenceClassification comptenceClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string comptenceClassificationInfo = comptenceClassification.Name;

            try
            {
                IsDeleted = await _ComptenceClassificationRepo.Delete(comptenceClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ComptenceClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ComptenceClassificationsDelete_POST", errMessage);
                return View(comptenceClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + comptenceClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}