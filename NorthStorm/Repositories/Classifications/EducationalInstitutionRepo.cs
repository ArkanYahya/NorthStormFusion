using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class EducationalInstitutionRepo : IEducationalInstitution
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public EducationalInstitutionRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(EducationalInstitution item)
        {
            _errors = "";

            try
            {
                _context.EducationalInstitutions.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(EducationalInstitution item)
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


        public async Task<bool> Edit(EducationalInstitution item)
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


        private List<EducationalInstitution> DoSort(List<EducationalInstitution> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
                    break;
                case "ParentEducationalInstitutionId":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ParentEducationalInstitutionId).ToList();
                    else
                        items = items.OrderByDescending(n => n.ParentEducationalInstitutionId).ToList();
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

        public async Task<PaginatedList<EducationalInstitution>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<EducationalInstitution> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.EducationalInstitutions.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.ParentEducationalInstitution)
                    .Include(s => s.Location)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.EducationalInstitutions
                    .Include(s => s.ParentEducationalInstitution)
                    .Include(s => s.Location)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<EducationalInstitution> retItems = new PaginatedList<EducationalInstitution>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<EducationalInstitution> GetItem(int Id)
        {
            EducationalInstitution item = await _context.EducationalInstitutions
                .Include(s => s.ParentEducationalInstitution)
                .Include(s => s.Location)
                .Include(s => s.Classification)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
