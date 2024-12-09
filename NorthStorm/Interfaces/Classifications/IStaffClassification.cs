using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IStaffClassification
    {
        public string GetErrors();

        Task<PaginatedList<StaffClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<StaffClassification> GetItem(int id); // read particular item

        Task<bool> Create(StaffClassification item);
        Task<bool> Edit(StaffClassification item);
        Task<bool> Delete(StaffClassification item);

    }
}
