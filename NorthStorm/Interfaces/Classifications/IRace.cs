using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IRace
    {
        public string GetErrors();

        Task<PaginatedList<Race>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Race> GetItem(int id); // read particular item

        Task<bool> Create(Race item);
        Task<bool> Edit(Race item);
        Task<bool> Delete(Race item);

    }
}
