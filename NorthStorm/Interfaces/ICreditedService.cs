using NorthStorm.Models;

using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface ICreditedService
    {
        public string GetErrors();

        Task<PaginatedList<CreditedService>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<CreditedService> GetItem(int id); // read particular item

        Task<bool> Create(CreditedService item);
        Task<bool> Edit(CreditedService item);
        Task<bool> Delete(CreditedService item);
        Task<bool> Update(CreditedService item);

    }
}
