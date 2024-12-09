using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class JobPositionRepo : IJobPosition
    {
        private string _errors = "";
        
        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public JobPositionRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(JobPosition jobPosition)
        {
            _errors = "";

            try
            {
                _context.JobPositions.Add(jobPosition);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(JobPosition jobPosition)
        {
            _errors = "";

            try
            {
                _context.Attach(jobPosition);
                _context.Entry(jobPosition).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(JobPosition jobPosition)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == jobPosition.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(jobPosition);
                _context.Entry(jobPosition).State = EntityState.Modified;
                _context.Employees.AddRange(jobPosition.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(JobPosition jobPosition)
        {
            _errors = "";

            try
            {
                _context.JobPositions.Update(jobPosition);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(jobPosition);
                //_context.Entry(jobPosition).State = EntityState.Modified;
                //_context.Employees.AddRange(jobPosition.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<JobPosition> DoSort(List<JobPosition> items, string SortProperty, SortOrder sortOrder)
        {
            switch (SortProperty)
            {
                case "ReferenceNo":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ReferenceNo).ToList();
                    else
                        items = items.OrderByDescending(n => n.ReferenceNo).ToList();
                    break;
                case "ReferenceDate":
                    if (sortOrder == SortOrder.Ascending)
                        items = items.OrderBy(n => n.ReferenceDate).ToList();
                    else
                        items = items.OrderByDescending(n => n.ReferenceDate).ToList();
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

        public async Task<PaginatedList<JobPosition>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<JobPosition> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.JobPositions.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                    .Include(l => l.ResponsibleClassification)
                    .Include(w => w.DeputyClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.JobPositions
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
                    .Include(l => l.ResponsibleClassification)
                    .Include(w => w.DeputyClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<JobPosition> retItems = new PaginatedList<JobPosition>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<JobPosition> GetItem(int Id)
        {
            JobPosition item = await _context.JobPositions
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
