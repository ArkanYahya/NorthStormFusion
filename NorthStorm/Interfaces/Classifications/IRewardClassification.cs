using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IRewardClassification
    {
        public string GetErrors();

        Task<PaginatedList<RewardClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<RewardClassification> GetItem(int id); // read particular item

        Task<bool> Create(RewardClassification item);
        Task<bool> Edit(RewardClassification item);
        Task<bool> Delete(RewardClassification item);

    }
}
