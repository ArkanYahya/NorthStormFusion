using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Tmp
{
    public interface ITmpAppreciation
    {
        public string GetErrors();

        Task<PaginatedList<TmpAppreciation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<TmpAppreciation> GetItem(int id); // read particular item

        Task<bool> Create(TmpAppreciation item);
        Task<bool> Edit(TmpAppreciation item);
        Task<bool> Delete(TmpAppreciation item);

    }
}
