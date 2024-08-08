using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface ILevel
    {
        public string GetErrors();

        Task<PaginatedList<Level>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Level> GetItem(int id); // read particular item

        Task<bool> Create(Level item);
        Task<bool> Edit(Level item);
        Task<bool> Delete(Level item);

    }
}
