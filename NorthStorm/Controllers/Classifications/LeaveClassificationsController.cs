using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;


                     // LeaveClassification Controller


namespace NorthStorm.Controllers
{
    public class LeaveClassificationsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ILeaveClassification _LeaveClassificationRepo;

        public LeaveClassificationsController(NorthStormContext context, ILeaveClassification leaveClassificationRepo)
        {
            _context = context;
            _LeaveClassificationRepo = leaveClassificationRepo;
        }

        // GET: LeaveClassifications
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

            PaginatedList<LeaveClassification> items = await _LeaveClassificationRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);

        }

        // GET: LeaveClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveClassification = await _LeaveClassificationRepo.GetItem((int)id);

            if (leaveClassification == null)
            {
                return NotFound();
            }

            return View(leaveClassification);
        }

        // GET: LeaveClassifications/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: LeaveClassifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, GenderId, LeaveIndays, ServiceCharged, PromotionCharged, OutSideLaborForce, SalaryCharged, OtherNotes")] LeaveClassification leaveClassification)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _LeaveClassificationRepo.Create(leaveClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _LeaveClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LeaveClassificationsCreate_POST", errMessage);

                    return View(leaveClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الاجازة " + leaveClassification.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(leaveClassification.GenderId);
                return View(leaveClassification);
            }
        }

        // GET: LeaveClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveClassification = await _LeaveClassificationRepo.GetItem((int)id);

            if (leaveClassification == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(leaveClassification.GenderId);
            return View(leaveClassification);
        }

        // POST: LeaveClassifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, GenderId, LeaveIndays, ServiceCharged, PromotionCharged,OutSideLaborForce, SalaryCharged, OtherNotes")] LeaveClassification leaveClassification)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != leaveClassification.Id)
			{
				return NotFound();
            }
            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _LeaveClassificationRepo.Edit(leaveClassification);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _LeaveClassificationRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("LeaveClassificationsEdit_POST", errMessage);

                    PopulateDropDownLists(leaveClassification.GenderId);
 
                    return View(leaveClassification);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الاجازة " + leaveClassification.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";
                
                PopulateDropDownLists(leaveClassification.GenderId);

                return View(leaveClassification);
            }
        }

        // GET: LeaveClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveClassification = await _LeaveClassificationRepo.GetItem((int)id);

            if (leaveClassification == null)
            {
                return NotFound();
            }
            
            PopulateDropDownLists(leaveClassification.GenderId);
            return View(leaveClassification);
        }

        // POST: LeaveClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(LeaveClassification leaveClassification)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string leaveClassificationInfo = leaveClassification.Name;

            try
            {
                IsDeleted = await _LeaveClassificationRepo.Delete(leaveClassification);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _LeaveClassificationRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("LeaveClassificationsDelete_POST", errMessage);
                return View(leaveClassification);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + leaveClassificationInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropDownLists(
          object selectedGender = null)

        {
            var gendersQuery = from g in _context.Genders
                               orderby g.Name
                               select g;
            ViewBag.GenderId = new SelectList(gendersQuery.AsNoTracking(), "Id", "Name", selectedGender);

        }
        }
    }