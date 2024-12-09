using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class EvaluationRepo : IEvaluation
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public EvaluationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(Evaluation evaluation)
        {
            _errors = "";

            try
            {
                _context.Evaluation.Add(evaluation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Evaluation evaluation)
        {
            _errors = "";

            try
            {
                _context.Attach(evaluation);
                _context.Entry(evaluation).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Evaluation evaluation)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == evaluation.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(evaluation);
                _context.Entry(evaluation).State = EntityState.Modified;
                _context.Employees.AddRange(evaluation.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(Evaluation evaluation)
        {
            _errors = "";

            try
            {
                _context.Evaluation.Update(evaluation);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(evaluation);
                //_context.Entry(evaluation).State = EntityState.Modified;
                //_context.Employees.AddRange(evaluation.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<Evaluation> DoSort(List<Evaluation> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<Evaluation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Evaluation> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Evaluation.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                    .Include(l => l.EvaluationClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Evaluation
                    .Include(s => s.Employees)
                    .Include(l => l.EvaluationClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Evaluation> retItems = new PaginatedList<Evaluation>(items, pageIndex, pageSize);

            return retItems;
        }

        public async Task<Evaluation> GetItem(int Id)
        {
            Evaluation item = await _context.Evaluation
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
