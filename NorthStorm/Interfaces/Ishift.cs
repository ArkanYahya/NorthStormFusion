using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IShift
    {
        public string GetErrors();

        Task<PaginatedList<Shift>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Shift> GetItem(int id); // read particular item

        Task<bool> Create(Shift item);
        Task<bool> Edit(Shift item);
        Task<bool> Delete(Shift item);
        Task<bool> Update(Shift item);

    }
}
