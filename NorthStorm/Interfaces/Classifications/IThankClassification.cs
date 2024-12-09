using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IThankClassification
    {
        public string GetErrors();

        Task<PaginatedList<ThankClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<ThankClassification> GetItem(int id); // read particular item

        Task<bool> Create(ThankClassification item);
        Task<bool> Edit(ThankClassification item);
        Task<bool> Delete(ThankClassification item);

    }
}
