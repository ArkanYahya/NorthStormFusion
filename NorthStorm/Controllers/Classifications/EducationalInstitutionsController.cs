using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;
using NorthStorm.Interfaces.Classifications;

namespace NorthStorm.Controllers
{
    public class EducationalInstitutionsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IEducationalInstitution _EducationalInstitutionRepo;

        public EducationalInstitutionsController(NorthStormContext context, IEducationalInstitution itemRepo)
        {
            _context = context;
            _EducationalInstitutionRepo = itemRepo;
        }

        public async Task<IActionResult> IndexTree()
        {

            var items = await _context.EducationalInstitutions
                    .Where(l => l.ParentEducationalInstitutionId == null)
                    .Include(l => l.ChildEducationalInstitutions)
                        .ThenInclude(c => c.ChildEducationalInstitutions)
                            .ThenInclude(c => c.ChildEducationalInstitutions)
                                .ThenInclude(c => c.ChildEducationalInstitutions) // Add more levels as needed
                                    .ThenInclude(c => c.ChildEducationalInstitutions) // Add more levels as needed
                                        .ThenInclude(c => c.ChildEducationalInstitutions) // Add more levels as needed
                                            .ThenInclude(c => c.ChildEducationalInstitutions) // Add more levels as needed
                    .ToListAsync();
            return View(items);
        }

        // GET: EducationalInstitutions
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 40)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.AddColumn("ParentEducationalInstitutionId");
            sortModel.AddColumn("ClassificationId");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<EducationalInstitution> items = await _EducationalInstitutionRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: EducationalInstitutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _EducationalInstitutionRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: EducationalInstitutions/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: EducationalInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, LocationId, ClassificationId, ParentEducationalInstitutionId, Classification, ParentEducationalInstitution, ChildEducationalInstitutions")] EducationalInstitution item)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _EducationalInstitutionRepo.Create(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _EducationalInstitutionRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("EducationalInstitutionsCreate_POST", errMessage);

                    PopulateDropDownLists(
                        item.LocationId,
                        item.ParentEducationalInstitutionId,
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
                    item.ParentEducationalInstitutionId,
                    item.ClassificationId);
                return View(item);
            }
        }

        // GET: EducationalInstitutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _EducationalInstitutionRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.LocationId,
                item.ParentEducationalInstitutionId,
                item.ClassificationId);

            return View(item);
        }

        // POST: EducationalInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, LocationId, ClassificationId, ParentEducationalInstitutionId, Classification, ParentEducationalInstitution, ChildEducationalInstitutions")] EducationalInstitution item)
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
                    IsEdited = await _EducationalInstitutionRepo.Edit(item);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _EducationalInstitutionRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("EducationalInstitutionsEdit_POST", errMessage);
                    PopulateDropDownLists(
                        item.LocationId,
                        item.ParentEducationalInstitutionId,
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
                    item.ParentEducationalInstitutionId,
                    item.ClassificationId);

                return View(item);
            }
        }

        // GET: EducationalInstitutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _EducationalInstitutionRepo.GetItem((int)id);

            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(
                item.LocationId,
                item.ParentEducationalInstitutionId,
                item.ClassificationId);
            return View(item);
        }

        // POST: EducationalInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(EducationalInstitution item)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string itemInfo = item.Name;

            try
            {
                IsDeleted = await _EducationalInstitutionRepo.Delete(item);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _EducationalInstitutionRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("EducationalInstitutionsDelete_POST", errMessage);
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
            object selectedParentEducationalInstitution = null,
            object selectedEducationalInstitutionClassifications = null
            )
        {
            var locationQuery = from n in _context.Locations
                                       orderby n.Name
                                       select n;
            ViewBag.Locations = new SelectList(locationQuery.AsNoTracking(), "Id", "Name", selectedLocation);

            var parentEducationalInstitutionQuery = from r in _context.EducationalInstitutions
                                      orderby r.Name
                                      select r;
            ViewBag.ParentEducationalInstitutions = new SelectList(parentEducationalInstitutionQuery.AsNoTracking(), "Id", "Name", selectedParentEducationalInstitution);

            var parentEducationalInstitutionClassificationsQuery = from r in _context.EducationalInstituteClassifications
                                   orderby r.Name
                                   select r;
            ViewBag.Classifications = new SelectList(parentEducationalInstitutionClassificationsQuery.AsNoTracking(), "Id", "Name", selectedEducationalInstitutionClassifications);
        }
    }
}