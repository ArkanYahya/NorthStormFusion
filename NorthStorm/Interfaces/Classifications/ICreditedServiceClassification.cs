using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ICreditedServiceClassification
    {
        public string GetErrors();

        Task<PaginatedList<CreditedServiceClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<CreditedServiceClassification> GetItem(int id); // read particular item

        Task<bool> Create(CreditedServiceClassification item);
        Task<bool> Edit(CreditedServiceClassification item);
        Task<bool> Delete(CreditedServiceClassification item);

    }
}
