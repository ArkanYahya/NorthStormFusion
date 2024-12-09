using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class StaffingRepo : IStaffing
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public StaffingRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(Staffing jobTransfer)
        {
            _errors = "";

            try
            {
                _context.Staffings.Add(jobTransfer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Staffing jobTransfer)
        {
            _errors = "";

            try
            {
                _context.Attach(jobTransfer);
                _context.Entry(jobTransfer).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Staffing jobTransfer)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == jobTransfer.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(jobTransfer);
                _context.Entry(jobTransfer).State = EntityState.Modified;
                _context.Employees.AddRange(jobTransfer.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(Staffing jobTransfer)
        {
            _errors = "";

            try
            {
                _context.Staffings.Update(jobTransfer);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(jobTransfer);
                //_context.Entry(jobTransfer).State = EntityState.Modified;
                //_context.Employees.AddRange(jobTransfer.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<Staffing> DoSort(List<Staffing> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<Staffing>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Staffing> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Staffings.Where(n =>
                n.ReferenceNo.Contains(SearchText))
                             .Include(s => s.Employees)
                             .Include(l => l.StaffingJobTitle)
                             .Include(n => n.StaffingUnit)
                             .AsNoTracking()
                             .ToListAsync();
            }
            else
            {
                items = await _context.Staffings
                    .Include(s => s.Employees)
                    .Include(l => l.StaffingJobTitle)
                    .Include(n => n.StaffingUnit)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Staffing> retItems = new PaginatedList<Staffing>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Staffing> GetItem(int Id)
        {
            Staffing item = await _context.Staffings
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
