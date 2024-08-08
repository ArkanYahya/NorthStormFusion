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
    public class GovernmentalInstituteClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IGovernmentalInstituteClassification _GovernmentalInstituteClassificationRepo;

        public GovernmentalInstituteClassificationsController(NorthStormContext context, IGovernmentalInstituteClassification governmentalInstituteClassification)
        {
            _context = context;
            _GovernmentalInstituteClassificationRepo = governmentalInstituteClassification;
        }

        // GET: GovernmentalInstituteClassification
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

            PaginatedList<GovernmentalInstituteClassification> items = await _GovernmentalInstituteClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: GovernmentalInstituteClassification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _GovernmentalInstituteClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // GET: GovernmentalInstituteClassification/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GovernmentalInstituteClassification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, KirkukSymbol, BaghdadSymbol")] GovernmentalInstituteClassification jobTitleClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _GovernmentalInstituteClassificationRepo.Create(jobTitleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _GovernmentalInstituteClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("GovernmentalInstituteClassificationCreate_POST", errMessage);

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

        // GET: GovernmentalInstituteClassification/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _GovernmentalInstituteClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // POST: GovernmentalInstituteClassification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, KirkukSymbol, BaghdadSymbol")] GovernmentalInstituteClassification jobTitleClassification)
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
                    IsEdited = await _GovernmentalInstituteClassificationRepo.Edit(jobTitleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _GovernmentalInstituteClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("GovernmentalInstituteClassificationEdit_POST", errMessage);

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

        // GET: GovernmentalInstituteClassification/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitleClassification = await _GovernmentalInstituteClassificationRepo.GetItem((int)id);

            if (jobTitleClassification == null)
            {
                return NotFound();
            }

            return View(jobTitleClassification);
        }

        // POST: GovernmentalInstituteClassification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GovernmentalInstituteClassification jobTitleClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string jobTitleClassificationInfo = jobTitleClassification.Name;

            try
            {
                IsDeleted = await _GovernmentalInstituteClassificationRepo.Delete(jobTitleClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _GovernmentalInstituteClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("GovernmentalInstituteClassificationDelete_POST", errMessage);
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