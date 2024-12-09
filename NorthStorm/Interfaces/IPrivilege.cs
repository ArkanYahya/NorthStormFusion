using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IPrivilege
    {
        public string GetErrors();

        Task<PaginatedList<Privilege>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Privilege> GetItem(int id); // read particular item

        Task<bool> Create(Privilege item);
        Task<bool> Edit(Privilege item);
        Task<bool> Delete(Privilege item);
        Task<bool> Update(Privilege item);

    }
}
