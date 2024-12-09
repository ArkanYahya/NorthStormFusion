using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Repositories.Classifications
{
    public class DismissClassificationRepo : IDismissClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public DismissClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(DismissClassification dismissClassification)
        {
            _errors = "";

            try
            {
                                     
                _context.DismissClassifications.Add(dismissClassification); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(DismissClassification dismissClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(dismissClassification);
                _context.Entry(dismissClassification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(DismissClassification dismissClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(dismissClassification);
                _context.Entry(dismissClassification).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }




        private List<DismissClassification> DoSort(List<DismissClassification> items, string SortProperty, SortOrder sortOrder)
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
        public async Task<PaginatedList<DismissClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<DismissClassification> items;

           
            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.DismissClassifications.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                      .Include(e => e.status)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.DismissClassifications
                    .AsNoTracking()
                       .Include(e => e.status)
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<DismissClassification> retItems = new PaginatedList<DismissClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<DismissClassification> GetItem(int Id)
        {
            DismissClassification item = await _context.DismissClassifications.Include(e => e.status)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }

    }
}
