using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;
using NorthStorm.ViewModels;

namespace NorthStorm.Repositories
{
    public class RecruitmentRepo : IRecruitment
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public RecruitmentRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(Recruitment recruitment)
        {
            _errors = "";

            try
            {
                _context.Recruitments.Add(recruitment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Recruitment recruitment)
        {
            _errors = "";

            try
            {
                _context.Attach(recruitment);
                _context.Entry(recruitment).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Recruitment recruitment)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == recruitment.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(recruitment);
                _context.Entry(recruitment).State = EntityState.Modified;
                _context.Employees.AddRange(recruitment.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(Recruitment recruitment)
        {
            _errors = "";

            try
            {
                _context.Recruitments.Update(recruitment);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(recruitment);
                //_context.Entry(recruitment).State = EntityState.Modified;
                //_context.Employees.AddRange(recruitment.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> UpdateView(Recruitment recruitment, RecruitmentVM recruitmentVM)
        {
            _errors = "";

            try
            {

                var employeeJobTitle = new EmployeeJobTitle
                {
                    EmployeeId = recruitmentVM.Employee.Id,
                    JobTitleId = recruitmentVM.JobTitleId,
                    JobTitleAssignedDate = recruitmentVM.ReferenceDate,
                    ReferenceNo = recruitmentVM.ReferenceNo,
                    ReferenceDate = recruitmentVM.ReferenceDate,
                };

                _context.Recruitments.Update(recruitment);
                _context.EmployeeJobTitles.Add(employeeJobTitle);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<Recruitment> DoSort(List<Recruitment> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "ReferenceNo":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ReferenceNo).ToList();
                    else
                        items = items.OrderByDescending(n => n.ReferenceNo).ToList();
                    break;
                case "ReferenceDate":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ReferenceDate).ToList();
                    else
                        items = items.OrderByDescending(n => n.ReferenceDate).ToList();
                    break;
                default:
                    if (sortOrder == SortOrder.Descending)
                        items = items.OrderByDescending(d => d.Id).ToList();
                    else
                        items = items.OrderBy(d => d.Id).ToList();
                    break;
            }

            return items;
        }

        public async Task<PaginatedList<Recruitment>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Recruitment> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Recruitments.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Recruitments
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Recruitment> retItems = new PaginatedList<Recruitment>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Recruitment> GetItem(int Id)
        {
            Recruitment item = await _context.Recruitments
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
