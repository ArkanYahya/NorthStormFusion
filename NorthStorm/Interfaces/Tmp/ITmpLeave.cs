using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Tmp
{
    public interface ITmpLeave
    {
        public string GetErrors();

        Task<PaginatedList<TmpLeave>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<TmpLeave> GetItem(int id); // read particular item

        Task<bool> Create(TmpLeave item);
        Task<bool> Edit(TmpLeave item);
        Task<bool> Delete(TmpLeave item);

    }
}
