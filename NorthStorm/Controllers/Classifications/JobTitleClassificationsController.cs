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
    public class JobTitleClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IJobTitleClassification _JobTitleClassificationRepo;

        public JobTitleClassificationsController(NorthStormContext context, IJobTitleClassification jobTitleClassificationRepo)
        {
            _context = context;
            _JobTitleClassificationRepo = jobTitleClassificationRepo;
        }

        // GET: JobTitleClassifications
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

            PaginatedList<JobTitleClassification> items = await _JobTitleClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: JobTitleClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _JobTitleClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // GET: JobTitleClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTitleClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, KirkukSymbol, BaghdadSymbol")] JobTitleClassification jobTitleClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _JobTitleClassificationRepo.Create(jobTitleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _JobTitleClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitleClassificationsCreate_POST", errMessage);

                    return View(jobTitleClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + jobTitleClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";

                return View(jobTitleClassification);
            }
        }

        // GET: JobTitleClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _JobTitleClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // POST: JobTitleClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, KirkukSymbol, BaghdadSymbol")] JobTitleClassification jobTitleClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != jobTitleClassification.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _JobTitleClassificationRepo.Edit(jobTitleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _JobTitleClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("JobTitleClassificationsEdit_POST", errMessage);

                    return View(jobTitleClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + jobTitleClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(jobTitleClassification);
            }
        }

        // GET: JobTitleClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _JobTitleClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // POST: JobTitleClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(JobTitleClassification jobTitleClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string jobTitleClassificationInfo = jobTitleClassification.Name;

            try
            {
                IsDeleted = await _JobTitleClassificationRepo.Delete(jobTitleClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _JobTitleClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("JobTitleClassificationsDelete_POST", errMessage);
                return View(jobTitleClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + jobTitleClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}