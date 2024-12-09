using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IAbsence
    {
        public string GetErrors();

        Task<PaginatedList<Absence>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Absence> GetItem(int id); // read particular item

        Task<bool> Create(Absence item);
        Task<bool> Edit(Absence item);
        Task<bool> Delete(Absence item);
        Task<bool> Update(Absence item);

    }
}
