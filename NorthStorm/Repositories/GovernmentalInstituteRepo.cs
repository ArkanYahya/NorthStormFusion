using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class GovernmentalInstituteRepo : IGovernmentalInstitute
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public GovernmentalInstituteRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(GovernmentalInstitute country)
        {
            _errors = "";

            try
            {
                _context.GovernmentalInstitutes.Add(country);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(GovernmentalInstitute country)
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


        public async Task<bool> Edit(GovernmentalInstitute country)
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


        private List<GovernmentalInstitute> DoSort(List<GovernmentalInstitute> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<GovernmentalInstitute>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<GovernmentalInstitute> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.GovernmentalInstitutes.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.Classification)
                    .Include(s => s.ParentGovernmentalInstitute)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.GovernmentalInstitutes
                    .Include(s => s.Classification)
                    .Include(s => s.ParentGovernmentalInstitute)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<GovernmentalInstitute> retItems = new PaginatedList<GovernmentalInstitute>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<GovernmentalInstitute> GetItem(int Id)
        {
            GovernmentalInstitute item = await _context.GovernmentalInstitutes
                    .Include(s => s.Classification)
                    .Include(s => s.ParentGovernmentalInstitute)
                    .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
