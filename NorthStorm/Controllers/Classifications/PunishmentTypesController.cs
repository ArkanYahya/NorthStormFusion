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


                     // PunishmentType Controller


namespace NorthStorm.Controllers
{
    public class PunishmentTypesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IPunishmentClassification _PunishmentTypeRepo;

        public PunishmentTypesController(NorthStormContext context, IPunishmentClassification punishmentTypeRepo)
        {
            _context = context;
            _PunishmentTypeRepo = punishmentTypeRepo;
        }

        // GET: PunishmentTypes
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("PunishmentSeverity");
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<PunishmentClassification> items = await _PunishmentTypeRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: PunishmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishmentType = await _PunishmentTypeRepo.GetItem((int)id);

            if (punishmentType == null)
            {
                return NotFound();
            }

            return View(punishmentType);
        }

        // GET: PunishmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PunishmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, PunishmentSeverity, PunishmentWriting, PunishmentEffect")] PunishmentClassification punishmentType)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _PunishmentTypeRepo.Create(punishmentType);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _PunishmentTypeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PunishmentTypesCreate_POST", errMessage);

                    return View(punishmentType);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة العقوبة " + punishmentType.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(punishmentType);
            }
        }

        // GET: PunishmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishmentType = await _PunishmentTypeRepo.GetItem((int)id);

            if (punishmentType == null)
            {
                return NotFound();
            }

            return View(punishmentType);
        }

        // POST: PunishmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, PunishmentSeverity, PunishmentWriting, PunishmentEffect")] PunishmentClassification punishmentType)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != punishmentType.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _PunishmentTypeRepo.Edit(punishmentType);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _PunishmentTypeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PunishmentTypesEdit_POST", errMessage);
                    return View(punishmentType);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث العقوبة " + punishmentType.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(punishmentType);
            }
        }

        // GET: PunishmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishmentType = await _PunishmentTypeRepo.GetItem((int)id);

            if (punishmentType == null)
            {
                return NotFound();
            }

            return View(punishmentType);
        }

        // POST: PunishmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(PunishmentClassification punishmentType)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string punishmentTypeInfo = punishmentType.Name;

            try
            {
                IsDeleted = await _PunishmentTypeRepo.Delete(punishmentType);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _PunishmentTypeRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("PunishmentTypesDelete_POST", errMessage);
                return View(punishmentType);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + punishmentTypeInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}