using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories.Classifications
{
    public class ComptenceClassificationRepo : IComptenceClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

        private readonly NorthStormContext _context; // for connecting to efcore.
        public ComptenceClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public async Task<bool> Create(ComptenceClassification comptenceClassification)
        {
            _errors = "";

            try
            {
                _context.ComptenceClassifications.Add(comptenceClassification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Delete(ComptenceClassification comptenceClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(comptenceClassification);
                _context.Entry(comptenceClassification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Edit(ComptenceClassification comptenceClassification)
        {
            _errors = "";

            try
            {
                //_context.Update(recruitment);
                _context.Attach(comptenceClassification);
                _context.Entry(comptenceClassification).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<ComptenceClassification> DoSort(List<ComptenceClassification> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
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

        public async Task<PaginatedList<ComptenceClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<ComptenceClassification> items;

            if (!string.IsNullOrEmpty(SearchText))
            {
                items = await _context.ComptenceClassifications.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.ComptenceClassifications
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<ComptenceClassification> retItems = new PaginatedList<ComptenceClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<ComptenceClassification> GetItem(int Id)
        {
            ComptenceClassification item = await _context.ComptenceClassifications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
