using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IGrade
    {
        public string GetErrors();

        Task<PaginatedList<Grade>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Grade> GetItem(int id); // read particular item

        Task<bool> Create(Grade item);
        Task<bool> Edit(Grade item);
        Task<bool> Delete(Grade item);

    }
}
