using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IRankOther
    {
        public string GetErrors();

        Task<PaginatedList<RankOther>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<RankOther> GetItem(int id); // read particular item

        Task<bool> Create(RankOther item);
        Task<bool> Edit(RankOther item);
        Task<bool> Delete(RankOther item);

    }
}
