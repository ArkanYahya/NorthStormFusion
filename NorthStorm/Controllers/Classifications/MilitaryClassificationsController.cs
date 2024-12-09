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

                                            // MilitaryClassifications Controller

namespace NorthStorm.Controllers.Classifications
{
     public class MilitaryClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IMilitaryClassification _MilitaryClassificationRepo;

        public MilitaryClassificationsController(NorthStormContext context, IMilitaryClassification militaryClassificationRepo)
        {
            _context = context;
            _MilitaryClassificationRepo = militaryClassificationRepo;
        }

        // GET: MilitaryClassifications
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

            PaginatedList<MilitaryClassification> items = await _MilitaryClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: MilitaryClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryClassification = await _MilitaryClassificationRepo.GetItem((int)id);

            if (militaryClassification == null)
            {
                return NotFound();
            }

            return View(militaryClassification);
        }

        // GET: MilitaryClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MilitaryClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] MilitaryClassification militaryClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _MilitaryClassificationRepo.Create(militaryClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _MilitaryClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("MilitaryClassificationsCreate_POST", errMessage);

                    return View(militaryClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الخدمة العسكرية " + militaryClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(militaryClassification);
            }
        }

        // GET: MilitaryClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryClassification = await _MilitaryClassificationRepo.GetItem((int)id);

            if (militaryClassification == null)
            {
                return NotFound();
            }

            return View(militaryClassification);
        }

        // POST: MilitaryClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] MilitaryClassification militaryClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != militaryClassification.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _MilitaryClassificationRepo.Edit(militaryClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _MilitaryClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("MilitaryClassificationsEdit_POST", errMessage);
                    return View(militaryClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الخدمة العسكرية " + militaryClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(militaryClassification);
            }
        }

        // GET: MilitaryClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryClassification = await _MilitaryClassificationRepo.GetItem((int)id);

            if (militaryClassification == null)
            {
                return NotFound();
            }

            return View(militaryClassification);
        }

        // POST: MilitaryClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(MilitaryClassification militaryClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string militaryClassificationInfo = militaryClassification.Name;

            try
            {
                IsDeleted = await _MilitaryClassificationRepo.Delete(militaryClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _MilitaryClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("MilitaryClassificationsDelete_POST", errMessage);
                return View(militaryClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + militaryClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}