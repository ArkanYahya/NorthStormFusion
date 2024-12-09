using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ICollegeName
    {
        public string GetErrors();

        Task<PaginatedList<CollegeName>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<CollegeName> GetItem(int id); // read particular item

        Task<bool> Create(CollegeName item);
        Task<bool> Edit(CollegeName item);
        Task<bool> Delete(CollegeName item);

    }
}
