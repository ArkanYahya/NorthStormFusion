using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface ILocation
    {
        public string GetErrors();

        Task<PaginatedList<Location>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Location> GetItem(int id); // read particular item

        Task<bool> Create(Location item);
        Task<bool> Edit(Location item);
        Task<bool> Delete(Location item);

    }
}
