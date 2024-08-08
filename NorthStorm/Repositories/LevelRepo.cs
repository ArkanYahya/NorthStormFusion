using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class LevelRepo : ILevel
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public LevelRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(Level item)
        {
            _errors = "";

            try
            {
                _context.Levels.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Level item)
        {
            _errors = "";

            try
            {
                _context.Attach(item);
                _context.Entry(item).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Level item)
        {
            _errors = "";

            try
            {
                _context.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<Level> DoSort(List<Level> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
                    break;
                case "ParentLevelId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ParentLevelId).ToList();
                    else
                        items = items.OrderByDescending(n => n.ParentLevelId).ToList();
                    break;
                case "ClassificationId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Classification.Rank).ToList();
                    else
                        items = items.OrderByDescending(n => n.Classification.Rank).ToList();
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

        public async Task<PaginatedList<Level>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Level> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Levels.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.ParentLevel)
                    .Include(s => s.Location)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Levels
                    .Include(s => s.ParentLevel)
                    .Include(s => s.Location)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Level> retItems = new PaginatedList<Level>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Level> GetItem(int Id)
        {
            Level item = await _context.Levels
                .Include(s => s.ParentLevel)
                .Include(s => s.Location)
                .Include(s => s.Classification)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
