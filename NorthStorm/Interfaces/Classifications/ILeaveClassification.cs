using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ILeaveClassification
    {
        public string GetErrors();

        Task<PaginatedList<LeaveClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<LeaveClassification> GetItem(int id); // read particular item

        Task<bool> Create(LeaveClassification item);
        Task<bool> Edit(LeaveClassification item);
        Task<bool> Delete(LeaveClassification item);

    }
}
