using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IGovernmentalInstitute
    {
        public string GetErrors();

        Task<PaginatedList<GovernmentalInstitute>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 20);
        Task<GovernmentalInstitute> GetItem(int id); // read particular item

        Task<bool> Create(GovernmentalInstitute item);
        Task<bool> Edit(GovernmentalInstitute item);
        Task<bool> Delete(GovernmentalInstitute item);

    }
}
