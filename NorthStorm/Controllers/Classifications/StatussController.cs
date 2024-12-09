using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Assistants;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers.Assistants
{
    public class StatussController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IStatus _StatusRepo;

        public StatussController(NorthStormContext context, IStatus itemRepo)
        {
            _context = context;
            _StatusRepo = itemRepo;
        }

        // GET: Statuss
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

            PaginatedList<Status> items = await _StatusRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Statuss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _StatusRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Statuss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statuss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Status item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _StatusRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _StatusRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StatussCreate_POST", errMessage);

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

        // GET: Statuss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _StatusRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Statuss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] Status item)
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
                    IsEdited = await _StatusRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _StatusRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("StatussEdit_POST", errMessage);

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

        // GET: Statuss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _StatusRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Statuss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Status item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _StatusRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _StatusRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("StatussDelete_POST", errMessage);
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