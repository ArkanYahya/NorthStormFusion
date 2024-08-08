using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ILevelClassification
    {
        public string GetErrors();

        Task<PaginatedList<LevelClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<LevelClassification> GetItem(int id); // read particular item

        Task<bool> Create(LevelClassification item);
        Task<bool> Edit(LevelClassification item);
        Task<bool> Delete(LevelClassification item);
    }
}
