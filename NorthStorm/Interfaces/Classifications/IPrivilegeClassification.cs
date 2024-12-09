using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IPrivilegeClassification
    {
        public string GetErrors();

        Task<PaginatedList<PrivilegeClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<PrivilegeClassification> GetItem(int id); // read particular item

        Task<bool> Create(PrivilegeClassification item);
        Task<bool> Edit(PrivilegeClassification item);
        Task<bool> Delete(PrivilegeClassification item);

    }
}
