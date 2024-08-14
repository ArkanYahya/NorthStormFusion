using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Interfaces.Tmp;

namespace NorthStorm.Repositories.Tmp
    {
    public class TmpAppreciationRepo : ITmpAppreciation
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public TmpAppreciationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(TmpAppreciation jobTransfer)
        {
            _errors = "";

            try
            {
                _context.TmpAppreciations.Add(jobTransfer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(TmpAppreciation jobTransfer)
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


        public async Task<bool> Edit(TmpAppreciation jobTransfer)
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

        public async Task<bool> Update(TmpAppreciation jobTransfer)
        {
            _errors = "";

            try
            {
                _context.TmpAppreciations.Update(jobTransfer);
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

        private List<TmpAppreciation> DoSort(List<TmpAppreciation> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "EmployeeId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.EmployeeId).ToList();
                    else
                        items = items.OrderByDescending(n => n.EmployeeId).ToList();
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

        public async Task<PaginatedList<TmpAppreciation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<TmpAppreciation> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.TmpAppreciations.Where(n =>
                n.Cause.Contains(SearchText))
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.TmpAppreciations
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<TmpAppreciation> retItems = new PaginatedList<TmpAppreciation>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<TmpAppreciation> GetItem(int Id)
        {
            TmpAppreciation item = await _context.TmpAppreciations
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
