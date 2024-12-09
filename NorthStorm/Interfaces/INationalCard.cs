using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface INationalCard
    {
        public string GetErrors();

        Task<PaginatedList<NationalCard>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<NationalCard> GetItem(int id); // read particular item

        Task<bool> Create(NationalCard item);
        Task<bool> Edit(NationalCard item);
        Task<bool> Delete(NationalCard item);
        Task<bool> Update(NationalCard item);

    }
}
