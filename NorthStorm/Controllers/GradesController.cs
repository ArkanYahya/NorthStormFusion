using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers
{
    public class GradesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IGrade _GradeRepo;

        public GradesController(NorthStormContext context, IGrade gradeRepo)
        {
            _context = context;
            _GradeRepo = gradeRepo;
        }

        // GET: Grades
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("GradeNumber");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Grade> items = await _GradeRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _GradeRepo.GetItem((int)id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
             
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeNumber,GradeAsWriting,Stage01, Stage02, Stage03, Stage04, Stage05, Stage06, Stage07, Stage08, Stage09, Stage10, Stage11, AnnualBonus, MinimumDuration ")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _GradeRepo.Create(grade);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _GradeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("GradesCreate_POST", errMessage);

                    return View(grade);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + grade.GradeNumber + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(grade);
            }
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _GradeRepo.GetItem((int)id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GradeNumber,GradeAsWriting,Stage01, Stage02, Stage03, Stage04, Stage05, Stage06, Stage07, Stage08, Stage09, Stage10, Stage11, AnnualBonus, MinimumDuration ")] Grade grade)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != grade.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _GradeRepo.Edit(grade);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _GradeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("GradesEdit_POST", errMessage);

                    return View(grade);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + grade.GradeNumber + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(grade);
            }
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _GradeRepo.GetItem((int)id);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Grade grade)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string gradeInfo = grade.GradeNumber.ToString();

            try
            {
                IsDeleted = await _GradeRepo.Delete(grade);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _GradeRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("GradesDelete_POST", errMessage);
                return View(grade);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + gradeInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}