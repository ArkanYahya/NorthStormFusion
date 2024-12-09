using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class ThankAndAppreciationRepo : IThankAndAppreciation
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public ThankAndAppreciationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(ThankAndAppreciation thankAndAppreciation)
        {
            _errors = "";

            try
            {
                _context.ThankAndAppreciations.Add(thankAndAppreciation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(ThankAndAppreciation thankAndAppreciation)
        {
            _errors = "";

            try
            {
                _context.Attach(thankAndAppreciation);
                _context.Entry(thankAndAppreciation).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(ThankAndAppreciation thankAndAppreciation)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == thankAndAppreciation.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(thankAndAppreciation);
                _context.Entry(thankAndAppreciation).State = EntityState.Modified;
                _context.Employees.AddRange(thankAndAppreciation.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(ThankAndAppreciation thankAndAppreciation)
        {
            _errors = "";

            try
            {
                _context.ThankAndAppreciations.Update(thankAndAppreciation);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(thankAndAppreciation);
                //_context.Entry(thankAndAppreciation).State = EntityState.Modified;
                //_context.Employees.AddRange(thankAndAppreciation.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<ThankAndAppreciation> DoSort(List<ThankAndAppreciation> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<ThankAndAppreciation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<ThankAndAppreciation> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.ThankAndAppreciations.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                    .Include(l => l.ThankClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.ThankAndAppreciations
                    .Include(s => s.Employees)
                    .Include(l => l.ThankClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<ThankAndAppreciation> retItems = new PaginatedList<ThankAndAppreciation>(items, pageIndex, pageSize);

            return retItems;
        }

        public async Task<ThankAndAppreciation> GetItem(int Id)
        {
            ThankAndAppreciation item = await _context.ThankAndAppreciations
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
