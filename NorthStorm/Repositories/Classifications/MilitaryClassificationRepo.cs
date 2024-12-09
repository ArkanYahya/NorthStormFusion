using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;


namespace NorthStorm.Repositories.Classifications
{
    public class MilitaryClassificationRepo : IMilitaryClassification
    {
        private string _errors = "";

        public string GetErrors()
        {
            return _errors;
        }


        private readonly NorthStormContext _context; // for connecting to efcore.
        public MilitaryClassificationRepo(NorthStormContext context) // will be passed by dependency injection.
        {
            _context = context;
        }


        public async Task<bool> Create(MilitaryClassification militaryClassification)
        {
            _errors = "";

            try
            {

                _context.MilitaryClassification.Add(militaryClassification); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Delete(MilitaryClassification militaryClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(militaryClassification);
                _context.Entry(militaryClassification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }


        public async Task<bool> Edit(MilitaryClassification militaryClassification)
        {
            _errors = "";

            try
            {
                _context.Attach(militaryClassification);
                _context.Entry(militaryClassification).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
            }
            return false;
        }




        private List<MilitaryClassification> DoSort(List<MilitaryClassification> items, string SortProperty, SortOrder sortOrder)
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

        public async Task<PaginatedList<MilitaryClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<MilitaryClassification> items;

            // تم اضافة حرف اس الى ملتري تايب سطر 124 و 132 و 147

            if (!String.IsNullOrEmpty(SearchText))
            {
                items = await _context.MilitaryClassification.Where(s =>
                s.Id.ToString().Equals(SearchText) ||
                s.Name.Contains(SearchText))
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                items = await _context.MilitaryClassification
                    .AsNoTracking()
                    .ToListAsync();
            }

            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<MilitaryClassification> retItems = new PaginatedList<MilitaryClassification>(items, pageIndex, pageSize);

            return retItems;
        }


        public async Task<MilitaryClassification> GetItem(int Id)
        {
            MilitaryClassification item = await _context.MilitaryClassification
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return item;
        }

    }
}
