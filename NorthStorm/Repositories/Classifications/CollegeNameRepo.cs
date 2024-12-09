using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Repositories.Classifications
{
    public class CollegeNameRepo : ICollegeName
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public CollegeNameRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(CollegeName collegeName)
        {
            _errors = "";

            try
            {
                _context.CollegeNames.Add(collegeName);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(CollegeName collegeName)
        {
            _errors = "";

            try
            {
                _context.Attach(collegeName);
                _context.Entry(collegeName).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(CollegeName collegeName)
        {
            _errors = "";

            try
            {
                _context.Attach(collegeName);
                _context.Entry(collegeName).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }




        private List<CollegeName> DoSort(List<CollegeName> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
                    break;
                //case "MotherName":
                //    if (sortOrder == SortOrder.Ascending)
                //        items = items.OrderBy(n => n.MotherName).ToList();
                //    else
                //        items = items.OrderByDescending(n => n.MotherName).ToList();
                //    break;
                default:
                    if (sortOrder == SortOrder.Descending)
                        items = items.OrderByDescending(d => d.Id).ToList();
                    else
                        items = items.OrderBy(d => d.Id).ToList();
                    break;
            }

            return items;
        }

        public async Task<PaginatedList<CollegeName>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<CollegeName> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.CollegeNames.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.CollegeNames
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<CollegeName> retItems = new PaginatedList<CollegeName>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<CollegeName> GetItem(int Id)
        {
            CollegeName item = await _context.CollegeNames
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }

    }
}
