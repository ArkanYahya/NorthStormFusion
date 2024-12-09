using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IComptenceClassification
    {
        public string GetErrors();

        Task<PaginatedList<ComptenceClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<ComptenceClassification> GetItem(int id); // read particular item

        Task<bool> Create(ComptenceClassification item);
        Task<bool> Edit(ComptenceClassification item);
        Task<bool> Delete(ComptenceClassification item);

    }
}
