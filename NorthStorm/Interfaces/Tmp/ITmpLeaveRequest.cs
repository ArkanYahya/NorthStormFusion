using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Tmp
{
    public interface ITmpLeaveRequest
    {
        public string GetErrors();

        Task<PaginatedList<TmpLeaveRequest>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<TmpLeaveRequest> GetItem(int id); // read particular item

        Task<bool> Create(TmpLeaveRequest item);
        Task<bool> Edit(TmpLeaveRequest item);
        Task<bool> Delete(TmpLeaveRequest item);

    }
}
