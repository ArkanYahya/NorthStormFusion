using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers.Classifications
{
    public class RankOthersController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IRankOther _RankOtherRepo;

        public RankOthersController(NorthStormContext context, IRankOther itemRepo)
        {
            _context = context;
            _RankOtherRepo = itemRepo;
        }

        // GET: RankOthers
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("RankAsNumber");
            sortModel.AddColumn("RankAsWriting");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Models.Assistants.RankOther> items = await _RankOtherRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: RankOthers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _RankOtherRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: RankOthers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RankOthers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankAsNumber, RankAsWriting, RankAsFemale, RankAsMale,RankAsDistinction, RankAsGeneral")] RankOther item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _RankOtherRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _RankOtherRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RankOthersCreate_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + item.RankAsWriting + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(item);
            }
        }

        // GET: RankOthers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _RankOtherRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: RankOthers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, RankAsNumber, RankAsWriting, RankAsFemale, RankAsMale,RankAsDistinction, RankAsGeneral")] RankOther item)
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
                    IsEdited = await _RankOtherRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _RankOtherRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RankOthersEdit_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + item.RankAsWriting + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(item);
            }
        }

        // GET: RankOthers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _RankOtherRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: RankOthers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(RankOther item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.RankAsWriting;

            try
            {
                IsDeleted = await _RankOtherRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _RankOtherRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("RankOthersDelete_POST", errMessage);
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