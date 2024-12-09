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

                                            // Careers Controller

namespace NorthStorm.Controllers.Classifications
{
     public class CareerClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ICareerClassification _CareerRepo;

        public CareerClassificationsController(NorthStormContext context, ICareerClassification careerRepo)
        {
            _context = context;
            _CareerRepo = careerRepo;
        }

        // GET: Careers
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
            //sortModel.AddColumn("MotherName");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<CareerClassification> items = await _CareerRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _CareerRepo.GetItem((int)id);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CareerClassification career)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _CareerRepo.Create(career);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _CareerRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CountriesCreate_POST", errMessage);

                    return View(career);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة المهنة/ الدور الوظيفي " + career.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(career);
            }
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _CareerRepo.GetItem((int)id);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] CareerClassification career)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != career.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _CareerRepo.Edit(career);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _CareerRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CountriesEdit_POST", errMessage);
                    return View(career);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث المهنة/ الدور الوظيفي " + career.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(career);
            }
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _CareerRepo.GetItem((int)id);

            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CareerClassification career)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string careerInfo = career.Name;

            try
            {
                IsDeleted = await _CareerRepo.Delete(career);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _CareerRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("CountriesDelete_POST", errMessage);
                return View(career);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + careerInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}