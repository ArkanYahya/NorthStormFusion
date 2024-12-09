using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IBeneficiaryClassification
    {
        public string GetErrors();

        Task<PaginatedList<BeneficiaryClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<BeneficiaryClassification> GetItem(int id); // read particular item

        Task<bool> Create(BeneficiaryClassification item);
        Task<bool> Edit(BeneficiaryClassification item);
        Task<bool> Delete(BeneficiaryClassification item);

    }
}
