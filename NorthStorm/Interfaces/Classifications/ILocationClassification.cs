using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ILocationClassification
    {
        public string GetErrors();

        Task<PaginatedList<LocationClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<LocationClassification> GetItem(int id); // read particular item

        Task<bool> Create(LocationClassification item);
        Task<bool> Edit(LocationClassification item);
        Task<bool> Delete(LocationClassification item);

    }
}
