using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class AbsenceRepo : IAbsence
    {
        private string _errors = "";
        
        public string GetErrors()
        {
            return _errors;
        }

#warning maybe I have to catch DbUpdateConcurrencyException too
        private readonly NorthStormContext _context; // for connecting to efcore.
        public AbsenceRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }
        public async Task<bool> Create(Absence absence)
        {
            _errors = "";

            try
            {
                _context.Absences.Add(absence);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(Absence absence)
        {
            _errors = "";

            try
            {
                _context.Attach(absence);
                _context.Entry(absence).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(Absence absence)
        {
            _errors = "";

            try
            {
                // remove deleted employees
                List<Employee> employees = await _context.Employees
                    .Where(d => d.Id == absence.Id).ToListAsync();
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();

                _context.Attach(absence);
                _context.Entry(absence).State = EntityState.Modified;
                _context.Employees.AddRange(absence.Employees);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        public async Task<bool> Update(Absence absence)
        {
            _errors = "";

            try
            {
                _context.Absences.Update(absence);
                await _context.SaveChangesAsync();
#warning check wether to delete this code or not
                //_context.Attach(absence);
                //_context.Entry(absence).State = EntityState.Modified;
                //_context.Employees.AddRange(absence.Employees);
                //await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }

        private List<Absence> DoSort(List<Absence> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<Absence>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Absence> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.Absences.Where(n =>
                n.ReferenceNo.Contains(SearchText) ||
                n.Subject.Contains(SearchText))
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
               //     .Include(l => l.AbsenceClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.Absences
                    .Include(s => s.Employees)
                        .ThenInclude(x => x.gender)
              //      .Include(l => l.AbsenceClassification)
                    .AsNoTracking()
                    .ToListAsync();
            }




            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Absence> retItems = new PaginatedList<Absence>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<Absence> GetItem(int Id)
        {
            Absence item = await _context.Absences
                     .Include(d => d.Employees)
                     .FirstOrDefaultAsync(i => i.Id == Id);
            return item;
        }



    }
}
