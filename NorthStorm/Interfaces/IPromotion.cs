using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IPromotion
    {
        public string GetErrors();

        Task<PaginatedList<Promotion>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Promotion> GetItem(int id); // read particular item

        Task<bool> Create(Promotion item);
        Task<bool> Edit(Promotion item);
        Task<bool> Delete(Promotion item);
        Task<bool> Update(Promotion item);

    }
}
