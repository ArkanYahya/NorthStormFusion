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
    public class JobTitlesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IJobTitle _JobTitleRepo;

        public JobTitlesController(NorthStormContext context, IJobTitle jobTitleRepo)
        {
            _context = context;
            _JobTitleRepo = jobTitleRepo;
        }

        public async Task<IActionResult> IndexTree()
        {

            var jobTitles = await _context.JobTitles
                    .Where(l => l.ParentJobTitleId == null)
                    .Include(l => l.ChildJobTitles)
                        .ThenInclude(c => c.ChildJobTitles)
                            .ThenInclude(c => c.ChildJobTitles)
                                .ThenInclude(c => c.ChildJobTitles) // Add more levels as needed
                                    .ThenInclude(c => c.ChildJobTitles) // Add more levels as needed
                                        .ThenInclude(c => c.ChildJobTitles) // Add more levels as needed
                                            .ThenInclude(c => c.ChildJobTitles) // Add more levels as needed
                    .ToListAsync();
            return View(jobTitles);
        }

        // GET: JobTitles
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            sortModel.AddColumn("Classification");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<JobTitle> items = await _JobTitleRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: JobTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _JobTitleRepo.GetItem((int)id);

            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // GET: JobTitles/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: JobTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, ClassificationId, GradeId, ParentJobTitleId, Classification, Grade, ParentJobTitle, ChildJobTitles")] JobTitle jobTitle)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _JobTitleRepo.Create(jobTitle);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _JobTitleRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitlesCreate_POST", errMessage);

                    PopulateDropDownLists(jobTitle.GradeId,
                        jobTitle.ClassificationId,
                        jobTitle.ParentJobTitleId);

                    return View(jobTitle);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + jobTitle.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(jobTitle.GradeId,
                    jobTitle.ClassificationId,
                    jobTitle.ParentJobTitleId);
                return View(jobTitle);
            }
        }

        // GET: JobTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _JobTitleRepo.GetItem((int)id);

            if (jobTitle == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(jobTitle.GradeId,
                jobTitle.ClassificationId,
                jobTitle.ParentJobTitleId);

            return View(jobTitle);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, ClassificationId, GradeId, ParentJobTitleId, Classification, Grade, ParentJobTitle, ChildJobTitles")] JobTitle jobTitle)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != jobTitle.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _JobTitleRepo.Edit(jobTitle);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _JobTitleRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitlesEdit_POST", errMessage);
                    PopulateDropDownLists(jobTitle.GradeId,
                        jobTitle.ClassificationId,
                        jobTitle.ParentJobTitleId);
                    return View(jobTitle);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + jobTitle.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                PopulateDropDownLists(jobTitle.GradeId,
                    jobTitle.ClassificationId,
                    jobTitle.ParentJobTitleId);

                return View(jobTitle);
            }
        }

        // GET: JobTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _JobTitleRepo.GetItem((int)id);

            if (jobTitle == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(jobTitle.GradeId,
                jobTitle.ClassificationId,
                jobTitle.ParentJobTitleId);
            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(JobTitle jobTitle)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string jobTitleInfo = jobTitle.Name;

            try
            {
                IsDeleted = await _JobTitleRepo.Delete(jobTitle);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _JobTitleRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("JobTitlesDelete_POST", errMessage);
                return View(jobTitle);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + jobTitleInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }


        private void PopulateDropDownLists(
            object selectedGrade = null,
            object selectedClassification = null,
            object selectedParentJobTitle = null
            )
        {
            var gradesQuery = from g in _context.Grades
                              orderby g.GradeNumber
                              select g;
            ViewBag.Grades = new SelectList(gradesQuery.AsNoTracking(), "Id", "GradeAsWriting", selectedGrade);

            var classificationsQuery = from n in _context.JobTitleClassifications
                                       orderby n.Name
                                       select n;
            ViewBag.Classifications = new SelectList(classificationsQuery.AsNoTracking(), "Id", "Name", selectedClassification);

            var parentJobTitleQuery = from r in _context.JobTitles
                                      orderby r.Name
                                      select r;
            ViewBag.ParentJobTitles = new SelectList(parentJobTitleQuery.AsNoTracking(), "Id", "Name", selectedParentJobTitle);
        }

        public JsonResult GetSubcategories(int categoryId)
        {
            var subcategories = _context.JobTitles
                .Include(x => x.Grade)
                .Where(s => s.Grade.GradeNumber == (categoryId-1))
                .ToList();
            return Json(subcategories);
        }
    }
}