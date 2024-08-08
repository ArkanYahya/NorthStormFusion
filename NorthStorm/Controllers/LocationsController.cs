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
    public class LocationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ILocation _LocationRepo;

        public LocationsController(NorthStormContext context, ILocation itemRepo)
        {
            _context = context;
            _LocationRepo = itemRepo;
        }

        public async Task<IActionResult> IndexTree()
        {

            var items = await _context.Locations
                    .Where(l => l.ParentLocationId == null)
                    .Include(l => l.ChildLocations)
                        .ThenInclude(c => c.ChildLocations)
                            .ThenInclude(c => c.ChildLocations)
                                .ThenInclude(c => c.ChildLocations) // Add more levels as needed
                                    .ThenInclude(c => c.ChildLocations) // Add more levels as needed
                                        .ThenInclude(c => c.ChildLocations) // Add more levels as needed
                                            .ThenInclude(c => c.ChildLocations) // Add more levels as needed
                    .ToListAsync();
            return View(items);
        }

        // GET: Locations
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.AddColumn("ClassificationId");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Location> items = await _LocationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LocationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, ClassificationId, ParentLocationId, Classification, ParentLocation")] Location item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _LocationRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _LocationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LocationsCreate_POST", errMessage);

                    PopulateDropDownLists(
                        item.ClassificationId,
                        item.ParentLocationId);

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
                    item.ClassificationId,
                    item.ParentLocationId);
                return View(item);
            }
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LocationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.ClassificationId,
                item.ParentLocationId);

            return View(item);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ClassificationId, ParentLocationId, Classification, ParentLocation")] Location item)
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
                    IsEdited = await _LocationRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _LocationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LocationsEdit_POST", errMessage);
                    PopulateDropDownLists(
                        item.ClassificationId,
                        item.ParentLocationId);
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
                    item.ClassificationId,
                    item.ParentLocationId);

                return View(item);
            }
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _LocationRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.ClassificationId,
                item.ParentLocationId);
            return View(item);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Location item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _LocationRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _LocationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("LocationsDelete_POST", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + itemInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }


        private void PopulateDropDownLists(
            object selectedClassification = null,
            object selectedParentLocation = null
            )
        {
            var classificationsQuery = from n in _context.LocationClassifications
                                       orderby n.Name
                                       select n;
            ViewBag.Classifications = new SelectList(classificationsQuery.AsNoTracking(), "Id", "Name", selectedClassification);

            var parentLocationQuery = from r in _context.Locations
                                      orderby r.Name
                                      select r;
            ViewBag.ParentLocations = new SelectList(parentLocationQuery.AsNoTracking(), "Id", "Name", selectedParentLocation);
        }
    }
}