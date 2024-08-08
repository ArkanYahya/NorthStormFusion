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
    public class EmployeesController : Controller
    {
        private readonly NorthStormContext _context;
        private readonly IEmployee _EmployeeRepo;

        public EmployeesController(NorthStormContext context, IEmployee employeeRepo)
        {
            _context = context;
            _EmployeeRepo = employeeRepo;
        }

        // GET: Employees
        public async Task<IActionResult> Index(
            string sortExpression = "",
            string SearchText = "",
            int pg = 1,
            int pageSize = 10)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("EmployeeId");
            sortModel.AddColumn("FullName");
            sortModel.AddColumn("MotherName");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewData["pageSize"] = pageSize;

            ViewBag.SearchText = SearchText;

            PaginatedList<Employee> items = await _EmployeeRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PagerModel(items.TotalRecords, pg, pageSize);

            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);

        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _EmployeeRepo.GetItem((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,FourthName,SurName,MotherFirstName,MotherMiddleName,MotherLastName,BirthDate,CivilNumber,IBAN,GenderId,ReligionId,RaceId,NationalityId,StatusId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool IsCreated = false;
                string errMessage = "";

                try
                {
                    IsCreated = await _EmployeeRepo.Create(employee);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsCreated == false)
                {
                    errMessage = errMessage + " " + _EmployeeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("EmployeesCreate_POST", errMessage);

                    PopulateDropDownLists(employee.GenderId,
                        employee.NationalityId,
                        employee.RaceId,
                        employee.ReligionId,
                        employee.StatusId);

                    return View(employee);
                }
                else
                {
                    TempData["SuccessMessage"] = "تمت إضافة القيد " + employee.FullName + " بنجاح.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "تأكد من صحة البيانات المدخلة";
                PopulateDropDownLists(employee.GenderId,
                    employee.NationalityId,
                    employee.RaceId,
                    employee.ReligionId,
                    employee.StatusId);
                return View(employee);
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _EmployeeRepo.GetItem((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(employee.GenderId,
                employee.NationalityId,
                employee.RaceId,
                employee.ReligionId,
                employee.StatusId);

            employee.IsCreateAction = false; // To stop duplicate validation
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,FourthName,SurName,MotherFirstName,MotherMiddleName,MotherLastName,BirthDate,CivilNumber,IBAN,GenderId,ReligionId,RaceId,NationalityId,StatusId, IsCreateAction")] Employee employee)
        {
#warning use DbUpdateException \ DbUpdateConcurrencyException and other exceptions for all catches

            if (id != employee.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                bool IsEdited = false;
                string errMessage = "";
                try
                {
                    IsEdited = await _EmployeeRepo.Edit(employee);
                }
                catch (Exception ex)
                {
                    errMessage = errMessage + " " + ex.Message;
                }

                if (IsEdited == false)
                {
                    errMessage = errMessage + " " + _EmployeeRepo.GetErrors();

                    TempData["ErrorMessage"] = errMessage;
                    ModelState.AddModelError("EmployeesEdit_POST", errMessage);
                    PopulateDropDownLists(employee.GenderId,
                        employee.NationalityId,
                        employee.RaceId,
                        employee.ReligionId,
                        employee.StatusId);
                    return View(employee);
                }
                else
                {
                    TempData["SuccessMessage"] = "تم تحديث القيد " + employee.FullName + " بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صالحة";

                PopulateDropDownLists(employee.GenderId,
                    employee.NationalityId,
                    employee.RaceId,
                    employee.ReligionId,
                    employee.StatusId);

                return View(employee);
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _EmployeeRepo.GetItem((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            PopulateDropDownLists(employee.GenderId,
                employee.NationalityId,
                employee.RaceId,
                employee.ReligionId,
                employee.StatusId);
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Employee employee)
        {
            bool IsDeleted = false;
            string errMessage = "";
            string employeeInfo = employee.FullName;

            try
            {
                IsDeleted = await _EmployeeRepo.Delete(employee);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }

            if (IsDeleted == false)
            {
                errMessage = errMessage + " " + _EmployeeRepo.GetErrors();

                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("EmployeesDelete_POST", errMessage);
                return View(employee);
            }
            else
            {
                TempData["SuccessMessage"] = "تم حذف القيد " + employeeInfo + " بنجاح";
                return RedirectToAction(nameof(Index));
            }
        }

        private void PopulateDropDownLists(
            object selectedGender = null,
            object selectedNationality = null,
            object selectedRace = null,
            object selectedReiligion = null,
            object selectedStatus = null//,
            //object selectedjobTitle =  null
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
            //ViewBag.jobTitles = new SelectList(jobTitlesQuery.AsNoTracking(), "Id", "Name", selectedjobTitle);
        }
    }
}