using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;

using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class CreditedServiceRepo : ICreditedService
    {
        private string _errors = "";
        
        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public CreditedServiceRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(CreditedService creditedService)
        {
            _errors = "";

            try
            {
                _context.CreditedServices.Add(creditedService);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(CreditedService creditedService)
        {
            _errors = "";

            try
            {
                _context.Attach(creditedService);
                _context.Entry(creditedService).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(CreditedService creditedService)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == creditedService.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(creditedService);
                _context.Entry(creditedService).State = EntityState.Modified;
                _context.Employees.AddRange(creditedService.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(CreditedService creditedService)
        {
            _errors = "";

            try
            {
                _context.CreditedServices.Update(creditedService);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(creditedService);
                //_context.Entry(creditedService).State = EntityState.Modified;
                //_context.Employees.AddRange(creditedService.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<CreditedService> DoSort(List<CreditedService> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<CreditedService>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<CreditedService> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.CreditedServices.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                    .Include(l => l.CreditedServiceClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.CreditedServices
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                    .Include(l => l.CreditedServiceClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<CreditedService> retItems = new PaginatedList<CreditedService>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<CreditedService> GetItem(int Id)
        {
            CreditedService item = await _context.CreditedServices
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
