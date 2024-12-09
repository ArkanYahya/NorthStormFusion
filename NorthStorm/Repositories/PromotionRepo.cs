using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class PromotionRepo : IPromotion
    {
        private string _errors = "";
        
        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public PromotionRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(Promotion Promotion)
        {
            _errors = "";

            try
            {
                _context.Promotions.Add(Promotion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Promotion Promotion)
        {
            _errors = "";

            try
            {
                _context.Attach(Promotion);
                _context.Entry(Promotion).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Promotion Promotion)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == Promotion.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(Promotion);
                _context.Entry(Promotion).State = EntityState.Modified;
                _context.Employees.AddRange(Promotion.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(Promotion Promotion)
        {
            _errors = "";

            try
            {
                _context.Promotions.Update(Promotion);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(Promotion);
                //_context.Entry(Promotion).State = EntityState.Modified;
                //_context.Employees.AddRange(Promotion.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<Promotion> DoSort(List<Promotion> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "BatchNo":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.BatchNo).ToList();
                    else
                        items = items.OrderByDescending(n => n.BatchNo).ToList();
                    break;
                case "PromotionMinutesYear":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.PromotionMinutesYear).ToList();
                    else
                        items = items.OrderByDescending(n => n.PromotionMinutesYear).ToList();
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

        public async Task<PaginatedList<Promotion>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Promotion> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Promotions.Where(n =>
                n.ReferenceNo.Contains(SearchText))
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                   
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Promotions
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                   
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Promotion> retItems = new PaginatedList<Promotion>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Promotion> GetItem(int Id)
        {
            Promotion item = await _context.Promotions
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
