using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IGeneralAndPrecise
    {
        public string GetErrors();

        Task<PaginatedList<GeneralAndPrecise>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<GeneralAndPrecise> GetItem(int id); // read particular item

        Task<bool> Create(GeneralAndPrecise item);
        Task<bool> Edit(GeneralAndPrecise item);
        Task<bool> Delete(GeneralAndPrecise item);

    }
}
