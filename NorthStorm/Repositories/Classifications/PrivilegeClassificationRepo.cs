using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;



namespace NorthStorm.Repositories.Classifications
{
    public class PrivilegeClassificationRepo : IPrivilegeClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public PrivilegeClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(PrivilegeClassification privilegeClassification)
        {
            _errors = "";

            try
            {
                _context.PrivilegeClassifications.Add(privilegeClassification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(PrivilegeClassification privilegeClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(privilegeClassification);
                _context.Entry(privilegeClassification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(PrivilegeClassification privilegeClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(privilegeClassification);
                _context.Entry(privilegeClassification).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }




        private List<PrivilegeClassification> DoSort(List<PrivilegeClassification> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<PrivilegeClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<PrivilegeClassification> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.PrivilegeClassifications.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.PrivilegeClassifications
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<PrivilegeClassification> retItems = new PaginatedList<PrivilegeClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<PrivilegeClassification> GetItem(int Id)
        {
            PrivilegeClassification item = await _context.PrivilegeClassifications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }

    }
}
