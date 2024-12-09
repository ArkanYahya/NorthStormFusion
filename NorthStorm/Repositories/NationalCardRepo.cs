using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class NationalCardRepo : INationalCard
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

        private readonly NorthStormContext _context; // for connecting to efcore.
        public NationalCardRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public async Task<bool> Create(NationalCard nationalCard)
        {
            _errors = "";

            try
            {
                _context.NationalCards.Add(nationalCard);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Delete(NationalCard nationalCard)
        {
            _errors = "";

            try
            {
                _context.Attach(nationalCard);
                _context.Entry(nationalCard).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Edit(NationalCard nationalCard)
        {
            _errors = "";

            try
            {
                //_context.Update(recruitment);
                _context.Attach(nationalCard);
                _context.Entry(nationalCard).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<NationalCard> DoSort(List<NationalCard> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "EmployeeId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.EmployeeId).ToList();
                    else
                        items = items.OrderByDescending(n => n.EmployeeId).ToList();
                    break;
                case "NationalCardNumber":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.NationalCardNumber).ToList();
                    else
                        items = items.OrderByDescending(n => n.NationalCardNumber).ToList();
                    break;

                case "CivilStatusIdNumber":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.CivilStatusIdNumber).ToList();
                    else
                        items = items.OrderByDescending(n => n.CivilStatusIdNumber).ToList();
                    break;

                default:
                    if (sortOrder == SortOrder.Descending)
                        items = items.OrderByDescending(d => d.EmployeeId).ToList();
                    else
                        items = items.OrderBy(d => d.EmployeeId).ToList();
                    break;
            }

            return items;
        }

        public async Task<PaginatedList<NationalCard>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<NationalCard> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.NationalCards.Where(s =>
                s.EmployeeId.ToString().Equals(SearchText) ||
                s.NationalCardNumber.ToString().Contains(SearchText) ||
                s.CivilStatusIdNumber.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.NationalCards
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<NationalCard> retItems = new PaginatedList<NationalCard>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<NationalCard> GetItem(int EmployeeId)
        {
            NationalCard item = await _context.NationalCards
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmployeeId == EmployeeId);
            return item;
        }

        public Task<bool> Update(NationalCard item)
        {
            throw new NotImplementedException();
        }
    }
}
