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
    public class BeneficiaryClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IBeneficiaryClassification _BeneficiaryClassificationRepo;

        public BeneficiaryClassificationsController(NorthStormContext context, IBeneficiaryClassification itemRepo)
        {
            _context = context;
            _BeneficiaryClassificationRepo = itemRepo;
        }

        // GET: BeneficiaryClassifications
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

            PaginatedList<BeneficiaryClassification> items = await _BeneficiaryClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: BeneficiaryClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _BeneficiaryClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: BeneficiaryClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BeneficiaryClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] BeneficiaryClassification item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _BeneficiaryClassificationRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _BeneficiaryClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("BeneficiaryClassificationsCreate_POST", errMessage);

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

        // GET: BeneficiaryClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _BeneficiaryClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: BeneficiaryClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] BeneficiaryClassification item)
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
                    IsEdited = await _BeneficiaryClassificationRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _BeneficiaryClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("BeneficiaryClassificationsEdit_POST", errMessage);

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

        // GET: BeneficiaryClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _BeneficiaryClassificationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: BeneficiaryClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BeneficiaryClassification item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _BeneficiaryClassificationRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _BeneficiaryClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("BeneficiaryClassificationsDelete_POST", errMessage);
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