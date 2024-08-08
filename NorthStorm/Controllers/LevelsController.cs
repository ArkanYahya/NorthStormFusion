using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers
{
    public class LevelsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ILevel _LevelRepo;

        public LevelsController(NorthStormContext context, ILevel itemRepo)
        {
            _context = context;
            _LevelRepo = itemRepo;
        }

        public async Task<IActionResult> IndexTree()
        {

            var items = await _context.Levels
                    .Where(l => l.ParentLevelId == null)
                    .Include(l => l.ChildLevels)
                        .ThenInclude(c => c.ChildLevels)
                            .ThenInclude(c => c.ChildLevels)
                                .ThenInclude(c => c.ChildLevels) // Add more levels as needed
                                    .ThenInclude(c => c.ChildLevels) // Add more levels as needed
                                        .ThenInclude(c => c.ChildLevels) // Add more levels as needed
                                            .ThenInclude(c => c.ChildLevels) // Add more levels as needed
                    .ToListAsync();
            return View(items);
        }

        // GET: Levels
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.AddColumn("ParentLevelId");
            sortModel.AddColumn("ClassificationId");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Level> items = await _LevelRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Levels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LevelRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Levels/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, LocationId, ClassificationId, GradeId, ParentLevelId, Classification, Grade, ParentLevel, ChildLevels")] Level item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _LevelRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _LevelRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LevelsCreate_POST", errMessage);

                    PopulateDropDownLists(
                        item.LocationId,
                        item.ParentLevelId,
                        item.ClassificationId);

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
                PopulateDropDownLists(
                    item.LocationId,
                    item.ParentLevelId,
                    item.ClassificationId);
                return View(item);
            }
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LevelRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.LocationId,
                item.ParentLevelId,
                item.ClassificationId);

            return View(item);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, LocationId, Description, ClassificationId, GradeId, ParentLevelId, Classification, Grade, ParentLevel, ChildLevels")] Level item)
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
                    IsEdited = await _LevelRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _LevelRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LevelsEdit_POST", errMessage);
                    PopulateDropDownLists(
                        item.LocationId,
                        item.ParentLevelId,
                        item.ClassificationId);
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

                PopulateDropDownLists(
                    item.LocationId,
                    item.ParentLevelId,
                    item.ClassificationId);

                return View(item);
            }
        }

        // GET: Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LevelRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.LocationId,
                item.ParentLevelId,
                item.ClassificationId);
            return View(item);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Level item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _LevelRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _LevelRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("LevelsDelete_POST", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + itemInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }


        private void PopulateDropDownLists(
            object selectedLocation = null,
            object selectedParentLevel = null,
            object selectedLevelClassifications = null
            )
        {
            var locationQuery = from n in _context.Locations
                                       orderby n.Name
                                       select n;
            ViewBag.Locations = new SelectList(locationQuery.AsNoTracking(), "Id", "Name", selectedLocation);

            var parentLevelQuery = from r in _context.Levels
                                      orderby r.Name
                                      select r;
            ViewBag.ParentLevels = new SelectList(parentLevelQuery.AsNoTracking(), "Id", "Name", selectedParentLevel);

            var parentLevelClassificationsQuery = from r in _context.LevelClassifications
                                   orderby r.Name
                                   select r;
            ViewBag.Classifications = new SelectList(parentLevelClassificationsQuery.AsNoTracking(), "Id", "Name", selectedLevelClassifications);
        }
    }
}