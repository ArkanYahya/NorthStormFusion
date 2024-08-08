using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface INationality
    {
        public string GetErrors();

        Task<PaginatedList<Nationality>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Nationality> GetItem(int id); // read particular item

        Task<bool> Create(Nationality item);
        Task<bool> Edit(Nationality item);
        Task<bool> Delete(Nationality item);

    }
}
