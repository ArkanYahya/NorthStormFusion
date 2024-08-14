using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Tmp
{
    public interface ITmpPromotion
    {
        public string GetErrors();

        Task<PaginatedList<TmpPromotion>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<TmpPromotion> GetItem(int id); // read particular item

        Task<bool> Create(TmpPromotion item);
        Task<bool> Edit(TmpPromotion item);
        Task<bool> Delete(TmpPromotion item);

    }
}
