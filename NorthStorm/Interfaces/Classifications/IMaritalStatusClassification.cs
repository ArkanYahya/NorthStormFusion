using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IMaritalStatusClassification
    {
        public string GetErrors();

        Task<PaginatedList<MaritalStatusClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<MaritalStatusClassification> GetItem(int id); // read particular item

        Task<bool> Create(MaritalStatusClassification item);
        Task<bool> Edit(MaritalStatusClassification item);
        Task<bool> Delete(MaritalStatusClassification item);

    }
}
