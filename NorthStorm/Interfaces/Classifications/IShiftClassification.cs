using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IShiftClassification
    {
        public string GetErrors();

        Task<PaginatedList<ShiftClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<ShiftClassification> GetItem(int id); // read particular item

        Task<bool> Create(ShiftClassification item);
        Task<bool> Edit(ShiftClassification item);
        Task<bool> Delete(ShiftClassification item);

    }
}
