using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IReligion
    {
        public string GetErrors();

        Task<PaginatedList<Religion>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Religion> GetItem(int id); // read particular item

        Task<bool> Create(Religion item);
        Task<bool> Edit(Religion item);
        Task<bool> Delete(Religion item);

    }
}
