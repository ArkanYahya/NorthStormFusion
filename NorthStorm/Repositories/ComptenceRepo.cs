using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class ComptenceRepo : IComptence
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public ComptenceRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(Comptence comptence)
        {
            _errors = "";

            try
            {
                _context.Comptences.Add(comptence);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Comptence comptence)
        {
            _errors = "";

            try
            {
                _context.Attach(comptence);
                _context.Entry(comptence).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Comptence comptence)
        {
            _errors = "";

            try
            {
                _context.Attach(comptence);
                _context.Entry(comptence).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<Comptence> DoSort(List<Comptence> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<Comptence>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Comptence> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Comptences.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.ParentComptence)
                //    .Include(s => s.Grade)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Comptences
                    .Include(s => s.ParentComptence)
                  //  .Include(s => s.Grade)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Comptence> retItems = new PaginatedList<Comptence>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Comptence> GetItem(int Id)
        {
            Comptence item = await _context.Comptences
                .Include(s => s.ParentComptence)
             //   .Include(s => s.Grade)
                .Include(s => s.Classification)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
