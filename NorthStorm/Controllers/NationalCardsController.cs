using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

// NationalCards Controller
namespace NorthStorm.Controllers
{
    public class NationalCardsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly INationalCard _NationalCardRepo;

        public NationalCardsController(NorthStormContext context, INationalCard NationalCardRepo)
        {
            _context = context;
            _NationalCardRepo = NationalCardRepo;
        }

        // GET: NationalCards
        public async Task<IActionResult> Index(
            int? selectedId,
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("EmployeeId");
            sortModel.AddColumn("NationalCardNumber");
            sortModel.AddColumn("CivilStatusIdNumber");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            if (selectedId != null)
                ViewData["NationalCardId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<NationalCard> items = await _NationalCardRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: NationalCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var NationalCard = await _NationalCardRepo.GetItem((int)id);

            if (NationalCard == null)
            {
                return NotFound();
            }

            return View(NationalCard);
        }


        // GET: NationalCard/CreateMasterDetails
        public IActionResult Create()
        {
            PopulateDropDownLists();
            var item = new NationalCardCreateViewModel();
            return View(item);
        }

        // POST: NationalCard/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId, NationalCardNumber, CivilStatusIdNumber, FamilyIdNumber, NationalIdReleaseDate, NationalIdExpiryDate")] NationalCardCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var NationalCard = new NationalCard
                {
                    EmployeeId = viewmodel.EmployeeId,
                    NationalCardNumber = viewmodel.NationalCardNumber,
                    CivilStatusIdNumber = viewmodel.CivilStatusIdNumber,
                    FamilyIdNumber = viewmodel.FamilyIdNumber,
                    NationalIdReleaseDate = viewmodel.NationalIdReleaseDate,
                    NationalIdExpiryDate = viewmodel.NationalIdExpiryDate,
                    PlaceOfBirthId = viewmodel.PlaceOfBirthId,
                };

                try
                {
                    IsCreated = await _NationalCardRepo.Create(NationalCard);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _NationalCardRepo.GetErrors();

                    PopulateDropDownLists(NationalCard.EmployeeId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("NationalCardsCreate_POST", errMessage);

                    return View(NationalCard);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ معلومات الموظف " + NationalCard.EmployeeId + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                PopulateDropDownLists(viewmodel.EmployeeId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: NationalCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var NationalCard = await _NationalCardRepo.GetItem((int)id);

            if (NationalCard == null)
            {
                return NotFound();
            }

            //return View(NationalCard);

            var model = new NationalCardEditViewModel
            {
                EmployeeId = NationalCard.EmployeeId,
                NationalCardNumber = NationalCard.NationalCardNumber,
                CivilStatusIdNumber = NationalCard.CivilStatusIdNumber,
                FamilyIdNumber = NationalCard.FamilyIdNumber,
                NationalIdReleaseDate = NationalCard.NationalIdReleaseDate,
                NationalIdExpiryDate = NationalCard.NationalIdExpiryDate,
                PlaceOfBirthId = NationalCard.PlaceOfBirthId,
                Employee = NationalCard.Employee
            };
            PopulateDropDownLists(model.PlaceOfBirthId);
            return View(model);
        }

        // POST: NationalCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId, NationalCardNumber, CivilStatusIdNumber, FamilyIdNumber, NationalIdReleaseDate, NationalIdExpiryDate,")] NationalCardEditViewModel model)
        {
            if (id != model.PlaceOfBirthId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var NationalCard = await _context.NationalCards
                                .Include(jt => jt.Employee)
                                .FirstOrDefaultAsync(jt => jt.EmployeeId == id);

                    if (NationalCard == null)
                    {
                        return NotFound();
                    }

                    NationalCard.NationalCardNumber = model.NationalCardNumber;
                    NationalCard.CivilStatusIdNumber = model.CivilStatusIdNumber;
                    NationalCard.FamilyIdNumber = model.FamilyIdNumber;
                    NationalCard.NationalIdReleaseDate = model.NationalIdReleaseDate;
                    NationalCard.NationalIdExpiryDate = model.NationalIdExpiryDate;
                    NationalCard.PlaceOfBirthId = model.PlaceOfBirthId;


                    // Update the related employees
                    NationalCard.Employee = null;
                    NationalCard.Employee = _context.Employees.FirstOrDefault(e => model.Employee.Id == e.Id);

                    _context.Update(NationalCard);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _NationalCardRepo.Edit(NationalCard);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _NationalCardRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("NationalCardsEdit_POST", errMessage);
                    PopulateDropDownLists(model.PlaceOfBirthId);
                    return View(model);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ معلومات الموظف \n" + model.EmployeeId + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";
                PopulateDropDownLists(model.PlaceOfBirthId);

                return View(model);
            }
        }

        // GET: NationalCards/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var NationalCard = await _NationalCardRepo.GetItem((int)id);

            if (NationalCard == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(NationalCard.PlaceOfBirthId);

            return View(NationalCard);
        }

        // POST: NationalCards/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(NationalCard NationalCard)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string NationalCardInfo = "";// NationalCard.ReferenceNo + " في " + NationalCard.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _NationalCardRepo.Delete(NationalCard);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _NationalCardRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("NationalCardsDelete_POST", errMessage);
                return View(NationalCard);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف معلومات الموظف \n" + NationalCardInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        // Autocomplete action
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            var employees = _context.Employees
                                     .Where(e => e.Id.ToString().Contains(term) ||
                                          e.FirstName.Contains(term) ||
                                          e.MiddleName.Contains(term) ||
                                          e.LastName.Contains(term) ||
                                          e.FourthName.Contains(term) ||
                                          e.SurName.Contains(term) ||
      e.GenderId.ToString().Contains(term) ||   // تمت الاضافة للحصول على مفتاح الجنس 1=ذكر

                                          (e.FirstName + " " + e.MiddleName + " " + e.LastName + " " + e.FourthName + " " + e.SurName).Contains(term)
                                    )
                                    .Select(e => new
                                    {
                                        value = e.Id,
                                        name = e.FullName,
                                        gender = e.GenderId,    // اضافة
                                        label = e.Id + " - " + e.FullName
                                    })
                                    .ToList();
            return Json(employees);
        }



        private void PopulateDropDownLists(object selectedLocation = null)
        {
            var locationQuery = from n in _context.Locations
                                orderby n.Name
                                select n;
            ViewBag.Locations = new SelectList(locationQuery.AsNoTracking(), "Id", "Name", selectedLocation);

        }
    }

}
