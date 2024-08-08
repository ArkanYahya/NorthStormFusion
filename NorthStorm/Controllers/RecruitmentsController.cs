using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;
using NorthStorm.Repositories;
using NorthStorm.ViewModels;

namespace NorthStorm.Controllers
{
    public class RecruitmentsController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IRecruitment _RecruitmentRepo;

        public RecruitmentsController(NorthStormContext context, IRecruitment recruitmentRepo)
        {
            _context = context;
            _RecruitmentRepo = recruitmentRepo;
        }

        // GET: Recruitments
        public async Task<IActionResult> Index(
            int? selectedId,
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Id");
            sortModel.AddColumn("ReferenceNo");
            sortModel.AddColumn("ReferenceDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            if (selectedId != null)
                ViewData["RecruitmentId"] = selectedId.Value;

            ViewBag.SearchText = SearchText;

            PaginatedList<Recruitment> items = await _RecruitmentRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            return View(items);
        }

        // GET: Recruitments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _RecruitmentRepo.GetItem((int)id);

            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // GET: Recruitments/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();

            Recruitment recruitment = new Recruitment();
            recruitment.ReferenceDate = DateTime.Now;
            recruitment.Employees.Add(new Employee() { BirthDate = DateTime.Now });
            return View(recruitment);
        }

        // POST: Recruitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReferenceNo,ReferenceDate,Subject, Employees")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                // لحذف الموظفين الذين تم حذفهم من نافذة الادخال قبل الحفظ
                foreach (var item in recruitment.Employees)
                {
                    if (String.IsNullOrEmpty(item.FirstName))
                        recruitment.Employees.Remove(item);
                }

                // لتحميل قوائم الجنس والدين والقومية ... الخ
                PopulateDropDownLists();

                try
                {
                    IsCreated = await _RecruitmentRepo.Create(recruitment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RecruitmentsCreate_POST", errMessage);

                    PopulateDropDownLists();

                    return View(recruitment);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + recruitment.ReferenceNo + " المؤرخ في " + recruitment.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                PopulateDropDownLists();
                return View(recruitment);
            }
        }


        // GET: Recruitments/Create
        public IActionResult CreateMaster()
        {
            Recruitment recruitment = new Recruitment();
            recruitment.ReferenceDate = DateTime.Now;
            return View(recruitment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMaster([Bind("Id,ReferenceNo,ReferenceDate,Subject")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _RecruitmentRepo.Create(recruitment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RecruitmentsCreate_POST", errMessage);

                    return View(recruitment);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم حفظ الأمر ذي العدد " + recruitment.ReferenceNo + " المؤرخ في " + recruitment.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                return View(recruitment);
            }
        }


        public async Task<IActionResult> CreateDetails(int? SelectedRecruitment)
        {
            if (SelectedRecruitment == null)
            {
                return NotFound();
            }

            var recruitment = await _RecruitmentRepo.GetItem((int)SelectedRecruitment);

            if (recruitment == null)
            {
                return NotFound();
            }

            PopulateDropDownLists();

            recruitment.Employees.Add(new Employee() { BirthDate = DateTime.Now });

            return View(recruitment);
        }


        // POST: Recruitments/CreateDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails([Bind("Id,ReferenceNo,ReferenceDate,Subject, Employees")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                bool IsUpdated = false;
                string errMessage = "";

                try
                {
                    var selectedRecruitment = await _RecruitmentRepo.GetItem(recruitment.Id);
                    selectedRecruitment.Employees.Add(recruitment.Employees.ElementAt(0));

                    IsUpdated = await _RecruitmentRepo.Update(selectedRecruitment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                    ModelState.AddModelError("RecruitmentsCreateDetails_POST", ex.Message);
                }

                if (IsUpdated == false)
                {
                    errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RecruitmentsCreate_POST", errMessage);

                    PopulateDropDownLists(recruitment.Employees.ElementAt(0).GenderId,
                        recruitment.Employees.ElementAt(0).NationalityId,
                        recruitment.Employees.ElementAt(0).RaceId,
                        recruitment.Employees.ElementAt(0).ReligionId,
                        recruitment.Employees.ElementAt(0).StatusId);

                    return View(recruitment);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الموظف \n" + recruitment.Employees.ElementAt(0).FullName +
                        "\n على الأمر الإداري ذي العدد \n" + recruitment.ReferenceNo +
                        " في " + recruitment.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(recruitment.Employees.ElementAt(0).GenderId,
                    recruitment.Employees.ElementAt(0).NationalityId,
                    recruitment.Employees.ElementAt(0).RaceId,
                    recruitment.Employees.ElementAt(0).ReligionId,
                    recruitment.Employees.ElementAt(0).StatusId);

                return View(recruitment);
            }
        }



        public async Task<IActionResult> CreateView(int? SelectedRecruitment)
        {
            if (SelectedRecruitment == null)
            {
                return NotFound();
            }

            var recruitment = await _RecruitmentRepo.GetItem((int)SelectedRecruitment);

            if (recruitment == null)
            {
                return NotFound();
            }

            RecruitmentVM viewModel = new RecruitmentVM
            {
                Id = recruitment.Id,
                ReferenceNo = recruitment.ReferenceNo,
                ReferenceDate = recruitment.ReferenceDate,
                Subject = recruitment.Subject,
            };

            PopulateDropDownLists();

            viewModel.Employee.BirthDate = DateTime.Now;


            return View(viewModel);
        }

        // POST: Recruitments/CreateDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateView([Bind("Id,ReferenceNo,ReferenceDate,Subject, Employee, JobTitleId")] RecruitmentVM recruitmentViewModel)
        {
            if (ModelState.IsValid)
            {
                bool IsUpdated = false;
                string errMessage = "";

                try
                {
#warning no need to include all employee, add GetItemNoChild 
                    var selectedRecruitment = await _RecruitmentRepo.GetItem(recruitmentViewModel.Id);
                    selectedRecruitment.Employees.Add(recruitmentViewModel.Employee);

                    IsUpdated = await _RecruitmentRepo.UpdateView(selectedRecruitment, recruitmentViewModel);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                    ModelState.AddModelError("RecruitmentsCreateDetails_POST", ex.Message);
                }

                if (IsUpdated == false)
                {
                    errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RecruitmentsCreate_POST", errMessage);

                    PopulateDropDownLists(recruitmentViewModel.Employee.GenderId,
                        recruitmentViewModel.Employee.NationalityId,
                        recruitmentViewModel.Employee.RaceId,
                        recruitmentViewModel.Employee.ReligionId,
                        recruitmentViewModel.Employee.StatusId);

                    return View(recruitmentViewModel);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة الموظف \n" + recruitmentViewModel.Employee.FullName +
                        "\n على الأمر الإداري ذي العدد \n" + recruitmentViewModel.ReferenceNo +
                        " في " + recruitmentViewModel.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(recruitmentViewModel.Employee.GenderId,
                    recruitmentViewModel.Employee.NationalityId,
                    recruitmentViewModel.Employee.RaceId,
                    recruitmentViewModel.Employee.ReligionId,
                    recruitmentViewModel.Employee.StatusId);

                return View(recruitmentViewModel);
            }
        }


        // GET: Recruitments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _RecruitmentRepo.GetItem((int)id);

            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // POST: Recruitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferenceNo,ReferenceDate,Subject")] Recruitment recruitment)
        {
            if (id != recruitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _RecruitmentRepo.Edit(recruitment);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("RecruitmentsEdit_POST", errMessage);

                    return View(recruitment);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث الأمر ذي العدد \n" + recruitment.ReferenceNo + " المؤرخ في" + recruitment.ReferenceDate.ToString("dd/MM/yyyy") + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                return View(recruitment);
            }
        }

        // GET: Recruitments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitment = await _RecruitmentRepo.GetItem((int)id);

            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // POST: Recruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Recruitment recruitment)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string recruitmentInfo = recruitment.ReferenceNo + " في " + recruitment.ReferenceDate.ToString("dd/MM/yyyy");

            try
            {
                IsDeleted = await _RecruitmentRepo.Delete(recruitment);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _RecruitmentRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("RecruitmentsDelete_POST", errMessage);
                return View(recruitment);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف الأمر \n" + recruitmentInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropDownLists(
            object selectedGender = null,
            object selectedNationality = null,
            object selectedRace = null,
            object selectedReiligion = null,
            object selectedStatus = null,
            object selectedJobTitle = null
            )
        {
            var gendersQuery = from g in _context.Genders
                               orderby g.Name
                               select g;
            ViewBag.GenderId = new SelectList(gendersQuery.AsNoTracking(), "Id", "Name", selectedGender);

            var nationalitiesQuery = from n in _context.Nationalities
                                     orderby n.Name
                                     select n;
            ViewBag.NationalityId = new SelectList(nationalitiesQuery.AsNoTracking(), "Id", "Name", selectedNationality);

            var racesQuery = from r in _context.Races
                             orderby r.Name
                             select r;
            ViewBag.RaceId = new SelectList(racesQuery.AsNoTracking(), "Id", "Name", selectedRace);

            var reiligionsQuery = from n in _context.Religions
                                  orderby n.Name
                                  select n;
            ViewBag.ReligionId = new SelectList(reiligionsQuery.AsNoTracking(), "Id", "Name", selectedReiligion);

            var statusesQuery = from n in _context.Statuses
                                orderby n.Name
                                select n;
            ViewBag.StatusId = new SelectList(statusesQuery.AsNoTracking(), "Id", "Name", selectedStatus);

            var jobTitlesQuery = from n in _context.JobTitles
                                 orderby n.Name
                                 select n;
            ViewBag.JobTitles = new SelectList(jobTitlesQuery.AsNoTracking(), "Id", "Name", selectedJobTitle);

        }

        private void PopulateEmployeeDropDownLists(
            object selectedEmp = null)
        {
            var emp = from c in _context.Employees
                      where c.Recruitment == null
                      select c;
            ViewBag.empId = new SelectList(emp.AsNoTracking(), "Id", "Name", selectedEmp);

        }
    }

}
