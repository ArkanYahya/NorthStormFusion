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

                                            // RewardClassifications Controller

namespace NorthStorm.Controllers.Classifications
{
     public class RewardClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IRewardClassification _RewardClassificationRepo;

        public RewardClassificationsController(NorthStormContext context, IRewardClassification staffTypeRepo)
        {
            _context = context;
            _RewardClassificationRepo = staffTypeRepo;
        }

        // GET: RewardClassifications
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

            PaginatedList<RewardClassification> items = await _RewardClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: RewardClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffType = await _RewardClassificationRepo.GetItem((int)id);

            if (staffType == null)
            {
                return NotFound();
            }

            return View(staffType);
        }

        // GET: RewardClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RewardClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, RewardPeriod")] RewardClassification staffType)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _RewardClassificationRepo.Create(staffType);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _RewardClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RewardClassificationsCreate_POST", errMessage);

                    return View(staffType);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة نوع الملاك " + staffType.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(staffType);
            }
        }

        // GET: RewardClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffType = await _RewardClassificationRepo.GetItem((int)id);

            if (staffType == null)
            {
                return NotFound();
            }

            return View(staffType);
        }

        // POST: RewardClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, RewardPeriod")] RewardClassification staffType)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != staffType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _RewardClassificationRepo.Edit(staffType);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _RewardClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RewardClassificationsEdit_POST", errMessage);
                    return View(staffType);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث نوع الملاك " + staffType.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(staffType);
            }
        }

        // GET: RewardClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffType = await _RewardClassificationRepo.GetItem((int)id);

            if (staffType == null)
            {
                return NotFound();
            }

            return View(staffType);
        }

        // POST: RewardClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(RewardClassification staffType)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string staffTypeInfo = staffType.Name;

            try
            {
                IsDeleted = await _RewardClassificationRepo.Delete(staffType);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _RewardClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("RewardClassificationsDelete_POST", errMessage);
                return View(staffType);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + staffTypeInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}