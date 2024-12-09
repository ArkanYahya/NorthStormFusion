using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories.Classifications
{
    public class EducationalInstitutionClassificationRepo : IEducationalInstitutionClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

        private readonly NorthStormContext _context; // for connecting to efcore.
        public EducationalInstitutionClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }

        public async Task<bool> Create(EducationalInstitutionClassification jobTitleClassification)
        {
            _errors = "";

            try
            {
                _context.EducationalInstituteClassifications.Add(jobTitleClassification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Delete(EducationalInstitutionClassification jobTitleClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(jobTitleClassification);
                _context.Entry(jobTitleClassification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Edit(EducationalInstitutionClassification jobTitleClassification)
        {
            _errors = "";

            try
            {
                //_context.Update(recruitment);
                _context.Attach(jobTitleClassification);
                _context.Entry(jobTitleClassification).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<EducationalInstitutionClassification> DoSort(List<EducationalInstitutionClassification> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "Name":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Name).ToList();
                    else
                        items = items.OrderByDescending(n => n.Name).ToList();
                    break;
                case "Rank":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.Rank).ToList();
                    else
                        items = items.OrderByDescending(n => n.Rank).ToList();
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

        public async Task<PaginatedList<EducationalInstitutionClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<EducationalInstitutionClassification> items;

            if (!string.IsNullOrEmpty(SearchText))
            {
                items = await _context.EducationalInstituteClassifications.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.EducationalInstituteClassifications
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<EducationalInstitutionClassification> retItems = new PaginatedList<EducationalInstitutionClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<EducationalInstitutionClassification> GetItem(int Id)
        {
            EducationalInstitutionClassification item = await _context.EducationalInstituteClassifications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
