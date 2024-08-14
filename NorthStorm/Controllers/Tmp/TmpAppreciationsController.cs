using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Interfaces.Tmp;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers.Tmp
{
    public class TmpAppreciationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ITmpAppreciation _TmpAppreciationRepo;

        public TmpAppreciationsController(NorthStormContext context, ITmpAppreciation itemRepo)
        {
            _context = context;
            _TmpAppreciationRepo = itemRepo;
        }

        // GET: TmpAppreciations
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

            PaginatedList<TmpAppreciation> items = await _TmpAppreciationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: TmpAppreciations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _TmpAppreciationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: TmpAppreciations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TmpAppreciations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cause")] TmpAppreciation item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _TmpAppreciationRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _TmpAppreciationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("TmpAppreciationsCreate_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + item.Cause + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(item);
            }
        }

        // GET: TmpAppreciations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _TmpAppreciationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: TmpAppreciations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Cause")] TmpAppreciation item)
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
                    IsEdited = await _TmpAppreciationRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _TmpAppreciationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("TmpAppreciationsEdit_POST", errMessage);

                    return View(item);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + item.Cause + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(item);
            }
        }

        // GET: TmpAppreciations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _TmpAppreciationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: TmpAppreciations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TmpAppreciation item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Cause;

            try
            {
                IsDeleted = await _TmpAppreciationRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _TmpAppreciationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("TmpAppreciationsDelete_POST", errMessage);
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