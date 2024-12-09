using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IRetirement
    {
        public string GetErrors();

        Task<PaginatedList<Retirement>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Retirement> GetItem(int id); // read particular item

        Task<bool> Create(Retirement item);
        Task<bool> Edit(Retirement item);
        Task<bool> Delete(Retirement item);
        Task<bool> Update(Retirement item);

    }
}
