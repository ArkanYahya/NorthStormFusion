using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IStaffing
    {
        public string GetErrors();

        Task<PaginatedList<Staffing>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Staffing> GetItem(int id); // read particular item

        Task<bool> Create(Staffing item);
        Task<bool> Edit(Staffing item);
        Task<bool> Delete(Staffing item);
        Task<bool> Update(Staffing item);

    }
}
