using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class LocationRepo : ILocation
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public LocationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(Location country)
        {
            _errors = "";

            try
            {
                _context.Locations.Add(country);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Location country)
        {
            _errors = "";

            try
            {
                _context.Attach(country);
                _context.Entry(country).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Location country)
        {
            _errors = "";

            try
            {
                _context.Attach(country);
                _context.Entry(country).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<Location> DoSort(List<Location> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
                    break;
                case "ClassificationId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Classification.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Classification.Name).ToList();
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

        public async Task<PaginatedList<Location>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Location> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Locations.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.Classification)
                    .Include(s => s.ParentLocation)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Locations
                    .Include(s => s.Classification)
                    .Include(s => s.ParentLocation)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Location> retItems = new PaginatedList<Location>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Location> GetItem(int Id)
        {
            Location item = await _context.Locations
                    .Include(s => s.Classification)
                    .Include(s => s.ParentLocation)
                    .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
