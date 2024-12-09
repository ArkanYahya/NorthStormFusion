using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Repositories.Classifications
{
    public class CareerClassificationRepo : ICareerClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public CareerClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(CareerClassification career)
        {
            _errors = "";

            try
            {
                _context.CareerClassifications.Add(career);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(CareerClassification career)
        {
            _errors = "";

            try
            {
                _context.Attach(career);
                _context.Entry(career).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(CareerClassification career)
        {
            _errors = "";

            try
            {
                _context.Attach(career);
                _context.Entry(career).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }




        private List<CareerClassification> DoSort(List<CareerClassification> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<CareerClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<CareerClassification> items;

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.CareerClassifications.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.CareerClassifications
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<CareerClassification> retItems = new PaginatedList<CareerClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<CareerClassification> GetItem(int Id)
        {
            CareerClassification item = await _context.CareerClassifications
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }

    }
}
