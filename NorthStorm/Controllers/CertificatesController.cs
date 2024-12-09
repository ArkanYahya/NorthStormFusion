using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly ICertificate _CertificateRepo;

        public CertificatesController(NorthStormContext context, ICertificate certificateRepo)
        {
            _context = context;
            _CertificateRepo = certificateRepo;
        }

        // GET: Certificates
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 25)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("Name");
			sortModel.AddColumn("InflueritialCertificate");
			sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Certificate> items = await _CertificateRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _CertificateRepo.GetItem((int)id);

            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // GET: Certificates/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,InflueritialCertificate, GradeStart,StageStart,GradeEnd,FirstPromotionDuration, AllocationPercentage,CertificateOldCode ")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _CertificateRepo.Create(certificate);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _CertificateRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CertificatesCreate_POST", errMessage);

                    return View(certificate);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + certificate.Name + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(certificate);
            }
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _CertificateRepo.GetItem((int)id);

            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, InflueritialCertificate, GradeStart, StageStart, GradeEnd,FirstPromotionDuration, AllocationPercentage ,CertificateOldCode")] Certificate certificate)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != certificate.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _CertificateRepo.Edit(certificate);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _CertificateRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("CertificatesEdit_POST", errMessage);

                    return View(certificate);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + certificate.Name + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(certificate);
            }
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _CertificateRepo.GetItem((int)id);

            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Certificate certificate)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string certificateInfo = certificate.Name.ToString();

            try
            {
                IsDeleted = await _CertificateRepo.Delete(certificate);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _CertificateRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("CertificatesDelete_POST", errMessage);
                return View(certificate);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + certificateInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}

