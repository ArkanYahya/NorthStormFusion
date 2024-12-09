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

                               // CollegeNames Controller

namespace NorthStorm.Controllers.Classifications
{
     public class CollegeNamesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ICollegeName _CollegeNameRepo;

        public CollegeNamesController(NorthStormContext context, ICollegeName collegeNameRepo)
        {
            _context = context;
            _CollegeNameRepo = collegeNameRepo;
        }

        // GET: CollegeNames
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

            PaginatedList<CollegeName> items = await _CollegeNameRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: CollegeNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeName = await _CollegeNameRepo.GetItem((int)id);

            if (collegeName == null)
            {
                return NotFound();
            }

            return View(collegeName);
        }

        // GET: CollegeNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollegeNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, CollegeOldCode, CollegeSPSS1")] CollegeName collegeName)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _CollegeNameRepo.Create(collegeName);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _CollegeNameRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CollegeNamesCreate_POST", errMessage);

                    return View(collegeName);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الكلية " + collegeName.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(collegeName);
            }
        }

        // GET: CollegeNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeName = await _CollegeNameRepo.GetItem((int)id);

            if (collegeName == null)
            {
                return NotFound();
            }

            return View(collegeName);
        }

        // POST: CollegeNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, CollegeOldCode, CollegeSPSS1")] CollegeName collegeName)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != collegeName.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _CollegeNameRepo.Edit(collegeName);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _CollegeNameRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CollegeNamesEdit_POST", errMessage);
                    return View(collegeName);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الكلية " + collegeName.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(collegeName);
            }
        }

        // GET: CollegeNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeName = await _CollegeNameRepo.GetItem((int)id);

            if (collegeName == null)
            {
                return NotFound();
            }

            return View(collegeName);
        }

        // POST: CollegeNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CollegeName collegeName)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string collegeNameInfo = collegeName.Name;

            try
            {
                IsDeleted = await _CollegeNameRepo.Delete(collegeName);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _CollegeNameRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("CollegeNamesDelete_POST", errMessage);
                return View(collegeName);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + collegeNameInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}