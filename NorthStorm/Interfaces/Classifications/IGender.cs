using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IGender
    {
        public string GetErrors();

        Task<PaginatedList<Gender>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Gender> GetItem(int id); // read particular item

        Task<bool> Create(Gender item);
        Task<bool> Edit(Gender item);
        Task<bool> Delete(Gender item);

    }
}
