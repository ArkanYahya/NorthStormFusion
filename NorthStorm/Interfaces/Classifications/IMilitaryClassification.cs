using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IMilitaryClassification
    {
        public string GetErrors();

        Task<PaginatedList<MilitaryClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<MilitaryClassification> GetItem(int id); // read particular item

        Task<bool> Create(MilitaryClassification item);
        Task<bool> Edit(MilitaryClassification item);
        Task<bool> Delete(MilitaryClassification item);

    }
}
