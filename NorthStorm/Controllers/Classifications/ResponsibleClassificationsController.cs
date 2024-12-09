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


                     // ResponsibleClassifications Controller
                     // old name ResponsibleClassifications

namespace NorthStorm.Controllers
{
    public class ResponsibleClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IResponsibleClassification _ResponsibleClassificationRepo;

        public ResponsibleClassificationsController(NorthStormContext context, IResponsibleClassification responsibleClassificationRepo)
        {
            _context = context;
            _ResponsibleClassificationRepo = responsibleClassificationRepo;
        }

        // GET: ResponsibleClassifications
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
      //      sortModel.AddColumn("ResponsibleSeverity");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<ResponsibleClassification> items = await _ResponsibleClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: ResponsibleClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleClassification = await _ResponsibleClassificationRepo.GetItem((int)id);

            if (responsibleClassification == null)
            {
                return NotFound();
            }

            return View(responsibleClassification);
        }

        // GET: ResponsibleClassifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResponsibleClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, ResponsiblityPercentage, ResponsiblSPSS1, ResponsiblSPSS2 , ResponsiblityInEnglish")] ResponsibleClassification responsibleClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _ResponsibleClassificationRepo.Create(responsibleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _ResponsibleClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ResponsibleClassificationsCreate_POST", errMessage);

                    return View(responsibleClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة المسؤولية " + responsibleClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(responsibleClassification);
            }
        }

        // GET: ResponsibleClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleClassification = await _ResponsibleClassificationRepo.GetItem((int)id);

            if (responsibleClassification == null)
            {
                return NotFound();
            }

            return View(responsibleClassification);
        }

        // POST: ResponsibleClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, ResponsiblityPercentage, ResponsiblSPSS1, ResponsiblSPSS2 , ResponsiblityInEnglish")] ResponsibleClassification responsibleClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != responsibleClassification.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _ResponsibleClassificationRepo.Edit(responsibleClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _ResponsibleClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("ResponsibleClassificationsEdit_POST", errMessage);
                    return View(responsibleClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث المسؤولية " + responsibleClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(responsibleClassification);
            }
        }

        // GET: ResponsibleClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsibleClassification = await _ResponsibleClassificationRepo.GetItem((int)id);

            if (responsibleClassification == null)
            {
                return NotFound();
            }

            return View(responsibleClassification);
        }

        // POST: ResponsibleClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ResponsibleClassification responsibleClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string responsibleClassificationInfo = responsibleClassification.Name;

            try
            {
                IsDeleted = await _ResponsibleClassificationRepo.Delete(responsibleClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _ResponsibleClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("ResponsibleClassificationsDelete_POST", errMessage);
                return View(responsibleClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + responsibleClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}