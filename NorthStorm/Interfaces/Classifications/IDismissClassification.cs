using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IDismissClassification
    {
        public string GetErrors();

        Task<PaginatedList<DismissClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<DismissClassification> GetItem(int id); // read particular item

        Task<bool> Create(DismissClassification item);
        Task<bool> Edit(DismissClassification item);
        Task<bool> Delete(DismissClassification item);

    }
}
