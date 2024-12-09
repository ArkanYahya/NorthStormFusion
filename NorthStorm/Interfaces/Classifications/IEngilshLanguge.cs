using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IEngilshLanguge
    {
        public string GetErrors();

        Task<PaginatedList<EngilshLanguge>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<EngilshLanguge> GetItem(int id); // read particular item

        Task<bool> Create(EngilshLanguge item);
        Task<bool> Edit(EngilshLanguge item);
        Task<bool> Delete(EngilshLanguge item);

    }
}
