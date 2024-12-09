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

namespace NorthStorm.Controllers.Classifications
{
    public class JobTitleChangeClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IJobTitleChangeClassification _JobTitleChangeClassificationRepo;

        public JobTitleChangeClassificationsController(NorthStormContext context, IJobTitleChangeClassification jobTitleChangeClassificationRepo)
        {
            _context = context;
            _JobTitleChangeClassificationRepo = jobTitleChangeClassificationRepo;
        }

        // GET: JobTitleChangeClassifications
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

            PaginatedList<JobTitleChangeClassification> items = await _JobTitleChangeClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: JobTitleChangeClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleChangeClassification = await _JobTitleChangeClassificationRepo.GetItem((int)id);

            if (jobTitleChangeClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleChangeClassification);
        }

        // GET: JobTitleChangeClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTitleChangeClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] JobTitleChangeClassification jobTitleChangeClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _JobTitleChangeClassificationRepo.Create(jobTitleChangeClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _JobTitleChangeClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitleChangeClassificationsCreate_POST", errMessage);

                    return View(jobTitleChangeClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + jobTitleChangeClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(jobTitleChangeClassification);
            }
        }

        // GET: JobTitleChangeClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleChangeClassification = await _JobTitleChangeClassificationRepo.GetItem((int)id);

            if (jobTitleChangeClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleChangeClassification);
        }

        // POST: JobTitleChangeClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] JobTitleChangeClassification jobTitleChangeClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != jobTitleChangeClassification.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _JobTitleChangeClassificationRepo.Edit(jobTitleChangeClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _JobTitleChangeClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitleChangeClassificationsEdit_POST", errMessage);

                    return View(jobTitleChangeClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + jobTitleChangeClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(jobTitleChangeClassification);
            }
        }

        // GET: JobTitleChangeClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleChangeClassification = await _JobTitleChangeClassificationRepo.GetItem((int)id);

            if (jobTitleChangeClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleChangeClassification);
        }

        // POST: JobTitleChangeClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(JobTitleChangeClassification jobTitleChangeClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string jobTitleChangeClassificationInfo = jobTitleChangeClassification.Name;

            try
            {
                IsDeleted = await _JobTitleChangeClassificationRepo.Delete(jobTitleChangeClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _JobTitleChangeClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("JobTitleChangeClassificationsDelete_POST", errMessage);
                return View(jobTitleChangeClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + jobTitleChangeClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}