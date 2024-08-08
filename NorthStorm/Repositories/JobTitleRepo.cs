using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class JobTitleRepo : IJobTitle
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public JobTitleRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(JobTitle jobTitle)
        {
            _errors = "";

            try
            {
                _context.JobTitles.Add(jobTitle);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(JobTitle jobTitle)
        {
            _errors = "";

            try
            {
                _context.Attach(jobTitle);
                _context.Entry(jobTitle).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(JobTitle jobTitle)
        {
            _errors = "";

            try
            {
                _context.Attach(jobTitle);
                _context.Entry(jobTitle).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        private List<JobTitle> DoSort(List<JobTitle> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<JobTitle>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<JobTitle> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.JobTitles.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .Include(s => s.ParentJobTitle)
                    .Include(s => s.Grade)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.JobTitles
                    .Include(s => s.ParentJobTitle)
                    .Include(s => s.Grade)
                    .Include(s => s.Classification)
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<JobTitle> retItems = new PaginatedList<JobTitle>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<JobTitle> GetItem(int Id)
        {
            JobTitle item = await _context.JobTitles
                .Include(s => s.ParentJobTitle)
                .Include(s => s.Grade)
                .Include(s => s.Classification)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }


    }
}
