using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface ILeave
    {
        public string GetErrors();

        Task<PaginatedList<Leave>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Leave> GetItem(int id); // read particular item

        Task<bool> Create(Leave item);
        Task<bool> Edit(Leave item);
        Task<bool> Delete(Leave item);
        Task<bool> Update(Leave item);

    }
}
