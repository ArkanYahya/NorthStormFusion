using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IJobTitleClassification
    {
        public string GetErrors();

        Task<PaginatedList<JobTitleClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<JobTitleClassification> GetItem(int id); // read particular item

        Task<bool> Create(JobTitleClassification item);
        Task<bool> Edit(JobTitleClassification item);
        Task<bool> Delete(JobTitleClassification item);

    }
}
