using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Tmp
{
    public interface ITmpBonus
    {
        public string GetErrors();

        Task<PaginatedList<TmpBonus>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<TmpBonus> GetItem(int id); // read particular item

        Task<bool> Create(TmpBonus item);
        Task<bool> Edit(TmpBonus item);
        Task<bool> Delete(TmpBonus item);

    }
}
