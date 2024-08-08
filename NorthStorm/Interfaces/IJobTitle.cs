using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IJobTitle
    {
        public string GetErrors();

        Task<PaginatedList<JobTitle>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<JobTitle> GetItem(int id); // read particular item

        Task<bool> Create(JobTitle item);
        Task<bool> Edit(JobTitle item);
        Task<bool> Delete(JobTitle item);

    }
}
