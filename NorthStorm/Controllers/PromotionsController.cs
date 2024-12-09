using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

                                // Promotions Controller
namespace NorthStorm.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IPromotion _PromotionRepo;

        public PromotionsController(NorthStormContext context, IPromotion PromotionRepo)
        {
            _context = context;
            _PromotionRepo = PromotionRepo;
        }

        // GET: Promotions
        public async Task<IActionResult> Index(
            int? selectedId,
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("BatchNo");
            sortModel.AddColumn("PromotionMinutesYear");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            if (selectedId != null)
                ViewData["PromotionId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Promotion> items = await _PromotionRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promotion = await _PromotionRepo.GetItem((int)id);

            if (Promotion == null)
            {
                return NotFound();
            }

            return View(Promotion);
        }


        // GET: Promotion/CreateMasterDetails
        public IActionResult Create()
        {
        //    PopulatePromotionList();
            var item = new PromotionCreateViewModel();
            return View(item);
        }

        // POST: Promotion/CreateMasterDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchNo,  PromotionMinutesYear,Subject, OrderNo, ReferenceNo, ReferenceDate, EmployeeIds")] PromotionCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                var Promotion = new Promotion
                {
                    BatchNo = viewmodel.BatchNo,
                    PromotionMinutesYear = viewmodel.PromotionMinutesYear,
                    Subject = viewmodel.Subject,
                    OrderNo = viewmodel.OrderNo,
                    ReferenceNo = viewmodel.ReferenceNo,
                    ReferenceDate = viewmodel.ReferenceDate,
                    Employees = await _context.Employees
                                             .Where(e => viewmodel.EmployeeIds.Contains(e.Id))
                                             .ToListAsync()
                };

                try
                {
                    IsCreated = await _PromotionRepo.Create(Promotion);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _PromotionRepo.GetErrors();

                    //      PopulatePromotionList(Promotion.PromotionClassificationId);
                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PromotionsCreate_POST", errMessage);

                    return View(Promotion);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ المحضر  " + Promotion.ReferenceNo + " لسنــة " + Promotion.PromotionMinutesYear + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                //    PopulatePromotionList(viewmodel.PromotionClassificationId);
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return View(viewmodel);
            }
        }


        // GET: Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promotion = await _PromotionRepo.GetItem((int)id);

            if (Promotion == null)
            {
                return NotFound();
            }

            //return View(Promotion);

            var model = new PromotionEditViewModel
            {
                PromotionId = Promotion.Id,
                BatchNo = Promotion.BatchNo,
                PromotionMinutesYear = Promotion.PromotionMinutesYear,
                Subject = Promotion.Subject,
                OrderNo = Promotion.OrderNo,
                ReferenceNo = Promotion.ReferenceNo,
                ReferenceDate = Promotion.ReferenceDate,

                Employees = Promotion.Employees.Select(e => new EmployeesInfo14
                {
                    EmployeeId = e.Id,
                    FullName = $"{e.FirstName} {e.MiddleName} {e.LastName} {e.FourthName} {e.SurName}"
                }).ToList()
            };
        //    PopulatePromotionList(model.PromotionId);
            return View(model);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotionId, BatchNo,  PromotionMinutesYear, Subject, OrderNo, ReferenceNo, ReferenceDate, Employees")] PromotionEditViewModel model)
        {
            if (id != model.PromotionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    var Promotion = await _context.Promotions
                                .Include(jt => jt.Employees)
                                .FirstOrDefaultAsync(jt => jt.Id == id);

                    if (Promotion == null)
                    {
                        return NotFound();
                    }

                    Promotion.BatchNo = model.BatchNo;
                    Promotion.PromotionMinutesYear = model.PromotionMinutesYear;
                    Promotion.Subject = model.Subject;
                    Promotion.OrderNo = model.OrderNo;
                    Promotion.ReferenceNo = model.ReferenceNo;
                    Promotion.ReferenceDate = model.ReferenceDate;


                    // Update the related employees
                    Promotion.Employees.Clear();
                    Promotion.Employees = await _context.Employees
                                                          .Where(e => model.Employees.Select(e => e.EmployeeId).Contains(e.Id))
                                                          .ToListAsync();
                    _context.Update(Promotion);
                    IsEdited = await _context.SaveChangesAsync() > 0;
                    //IsEdited = await _PromotionRepo.Edit(Promotion);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _PromotionRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("PromotionsEdit_POST", errMessage);
             //       PopulatePromotionList(model.PromotionId);
                    return View(model);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الأمر ذي العدد \n" + model.ReferenceNo + " لســنة" + model.PromotionMinutesYear + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";
         //       PopulatePromotionList(model.PromotionId);

                return View(model);
            }
        }

        // GET: Promotions/DeleteMasterDetails/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Promotion = await _PromotionRepo.GetItem((int)id);

            if (Promotion == null)
            {
                return NotFound();
            }

            //  PopulatePromotionList(Promotion.PromotionClassificationId);

            return View(Promotion);
        }

        // POST: Promotions/DeleteMasterDetails/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Promotion Promotion)
        {
            bool IsDeleted = false;
            string errMessage = "";
#warning    get referenceNo and Reference date anf fix alert
            string PromotionInfo = "";// Promotion.ReferenceNo + " في " + Promotion.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _PromotionRepo.Delete(Promotion);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _PromotionRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("PromotionsDelete_POST", errMessage);
                return View(Promotion);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف المحضر \n" + PromotionInfo + " بنجاح";
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
                                        gender = e.gender.Name,    // اضافة
                                        //dismissclassification = e.dismissClassification.Name,
                                        label = e.Id + " - " + e.FullName
                                    })
                                    .ToList();
            return Json(employees);
        }
     //   private static void PopulatePromotionList(object selectedPromotion = null)


    }

}
