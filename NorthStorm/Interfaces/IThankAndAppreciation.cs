using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IThankAndAppreciation
    {
        public string GetErrors();

        Task<PaginatedList<ThankAndAppreciation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<ThankAndAppreciation> GetItem(int id); // read particular item

        Task<bool> Create(ThankAndAppreciation item);
        Task<bool> Edit(ThankAndAppreciation item);
        Task<bool> Delete(ThankAndAppreciation item);
        Task<bool> Update(ThankAndAppreciation item);

    }
}
