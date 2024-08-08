using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class EmployeeRepo : IEmployee
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

        private readonly NorthStormContext _context; // for connecting to efcore.
        public EmployeeRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public async Task<bool> Create(Employee employee)
        {
            _errors = "";

            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Delete(Employee employee)
        {
            _errors = "";

            try
            {
                _context.Attach(employee);
                _context.Entry(employee).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Edit(Employee employee)
        {
            _errors = "";

            try
            {
                //_context.Update(recruitment);
                _context.Attach(employee);
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<Employee> DoSort(List<Employee> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "FullName":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.FullName).ToList();
                    else
                        items = items.OrderByDescending(n => n.FullName).ToList();
                    break;
                case "MotherName":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.MotherName).ToList();
                    else
                        items = items.OrderByDescending(n => n.MotherName).ToList();
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

        public async Task<PaginatedList<Employee>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Employee> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Employees.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.FirstName.Contains(SearchText) ||
                s.MiddleName.Contains(SearchText) ||
                s.LastName.Contains(SearchText) ||
                s.FourthName.Contains(SearchText) ||
                s.SurName.Contains(SearchText))
                    .Include(e => e.gender)
                    .Include(e => e.nationality)
                    .Include(e => e.race)
                    .Include(e => e.religion)
                    .Include(e => e.status)
                    //.Include(e => e.JobTitle)
                    //.Include(e => e.Level)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Employees
                    .Include(e => e.gender)
                    .Include(e => e.nationality)
                    .Include(e => e.race)
                    .Include(e => e.religion)
                    .Include(e => e.status)
                    //.Include(e => e.JobTitle)
                    //.Include(e => e.Level)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Employee> retItems = new PaginatedList<Employee>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Employee> GetItem(int Id)
        {
            Employee item = await _context.Employees
                .Include(e => e.gender)
                .Include(e => e.nationality)
                .Include(e => e.race)
                .Include(e => e.religion)
                .Include(e => e.status)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
