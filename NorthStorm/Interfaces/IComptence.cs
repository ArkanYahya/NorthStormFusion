using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IComptence
    {
        public string GetErrors();

        Task<PaginatedList<Comptence>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Comptence> GetItem(int id); // read particular item

        Task<bool> Create(Comptence item);
        Task<bool> Edit(Comptence item);
        Task<bool> Delete(Comptence item);

    }
}
