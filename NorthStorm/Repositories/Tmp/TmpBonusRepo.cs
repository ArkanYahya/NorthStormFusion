using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;
using NorthStorm.Interfaces.Tmp;

namespace NorthStorm.Repositories.Tmp
{
    public class TmpBonusRepo : ITmpBonus
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public TmpBonusRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(TmpBonus jobTransfer)
        {
            _errors = "";

            try
            {
                _context.TmpBonuses.Add(jobTransfer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(TmpBonus jobTransfer)
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


        public async Task<bool> Edit(TmpBonus jobTransfer)
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

        public async Task<bool> Update(TmpBonus jobTransfer)
        {
            _errors = "";

            try
            {
                _context.TmpBonuses.Update(jobTransfer);
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

        private List<TmpBonus> DoSort(List<TmpBonus> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<TmpBonus>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<TmpBonus> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.TmpBonuses
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.TmpBonuses
                    .Include(s => s.Employees)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<TmpBonus> retItems = new PaginatedList<TmpBonus>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<TmpBonus> GetItem(int Id)
        {
            TmpBonus item = await _context.TmpBonuses
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
