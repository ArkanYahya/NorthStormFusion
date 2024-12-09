using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IDeputyClassification
    {
        public string GetErrors();

        Task<PaginatedList<DeputyClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<DeputyClassification> GetItem(int id); // read particular item

        Task<bool> Create(DeputyClassification item);
        Task<bool> Edit(DeputyClassification item);
        Task<bool> Delete(DeputyClassification item);

    }
}
