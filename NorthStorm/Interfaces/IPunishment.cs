using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IPunishment
    {
        public string GetErrors();

        Task<PaginatedList<Punishment>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Punishment> GetItem(int id); // read particular item

        Task<bool> Create(Punishment item);
        Task<bool> Edit(Punishment item);
        Task<bool> Delete(Punishment item);
        Task<bool> Update(Punishment item);

    }
}
