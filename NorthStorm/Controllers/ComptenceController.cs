using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers
{
                            // Comptence Controller
       
    public class ComptencesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IComptence _ComptenceRepo;

        public ComptencesController(NorthStormContext context, IComptence comptenceRepo)
        {
            _context = context;
            _ComptenceRepo = comptenceRepo;
        }

        public async Task<IActionResult> IndexTree()
        {

            var comptences = await _context.Comptences
                    .Where(l => l.ParentComptenceId == null)
                    .Include(l => l.ChildComptences)
                        .ThenInclude(c => c.ChildComptences)
                            .ThenInclude(c => c.ChildComptences)
                                .ThenInclude(c => c.ChildComptences) // Add more levels as needed
                                    .ThenInclude(c => c.ChildComptences) // Add more levels as needed
                                        .ThenInclude(c => c.ChildComptences) // Add more levels as needed
                                            .ThenInclude(c => c.ChildComptences) // Add more levels as needed
                    .ToListAsync();
            return View(comptences);
        }

        // GET: Comptences
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.AddColumn("Classification");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Comptence> items = await _ComptenceRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Comptences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptence = await _ComptenceRepo.GetItem((int)id);

            if (comptence == null)
            {
                return NotFound();
            }

            return View(comptence);
        }

        // GET: Comptences/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Comptences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, ParentComptenceId, ClassificationId, ComptenceSPSS,, ChildComptences, ParentComptence, Classification")] Comptence comptence)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _ComptenceRepo.Create(comptence);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ComptenceRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ComptencesCreate_POST", errMessage);

                   PopulateDropDownLists(comptence.ClassificationId,
                                         comptence.ParentComptenceId);

                    return View(comptence);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + comptence.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(comptence.ClassificationId,
                                      comptence.ParentComptenceId);
                return View(comptence);
            }
        }

        // GET: Comptences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptence = await _ComptenceRepo.GetItem((int)id);

            if (comptence == null)
            {
                return NotFound();
            }

           PopulateDropDownLists(comptence.ClassificationId,
                                 comptence.ParentComptenceId);

            return View(comptence);
        }

        // POST: Comptences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ClassificationId, ComptenceSPSS, ParentComptenceId, Classification, ParentComptence, ChildComptences")] Comptence comptence)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != comptence.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _ComptenceRepo.Edit(comptence);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ComptenceRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ComptencesEdit_POST", errMessage);
                    PopulateDropDownLists(comptence.ClassificationId,
                                          comptence.ParentComptenceId);
                    return View(comptence);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + comptence.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

               PopulateDropDownLists(comptence.ClassificationId,
                                     comptence.ParentComptenceId);

                return View(comptence);
            }
        }

        // GET: Comptences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptence = await _ComptenceRepo.GetItem((int)id);

            if (comptence == null)
            {
                return NotFound();
            }

           PopulateDropDownLists(comptence.ClassificationId,
                                 comptence.ParentComptenceId);
            return View(comptence);
        }

        // POST: Comptences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Comptence comptence)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string comptenceInfo = comptence.Name;

            try
            {
                IsDeleted = await _ComptenceRepo.Delete(comptence);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ComptenceRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ComptencesDelete_POST", errMessage);
                return View(comptence);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + comptenceInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }


        private void PopulateDropDownLists(
            object selectedGrade = null,
            object selectedClassification = null,
            object selectedParentComptence = null
            )
        {
          //  var gradesQuery = from g in _context.Grades
          //                     orderby g.GradeNumber
          //                     select g;
          //  ViewBag.Grades = new SelectList(gradesQuery.AsNoTracking(), "Id", "GradeAsWriting", selectedGrade);

            var classificationsQuery = from n in _context.ComptenceClassifications
                                       orderby n.Name
                                     select n;
            ViewBag.Classifications = new SelectList(classificationsQuery.AsNoTracking(), "Id", "Name", selectedClassification);

            var parentComptenceQuery = from r in _context.Comptences
                             orderby r.Name
                             select r;
            ViewBag.ParentComptences = new SelectList(parentComptenceQuery.AsNoTracking(), "Id", "Name", selectedParentComptence);
        }
    }
}