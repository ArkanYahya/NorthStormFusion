using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IJobTitleChangeClassification
    {
        public string GetErrors();

        Task<PaginatedList<JobTitleChangeClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<JobTitleChangeClassification> GetItem(int id); // read particular item

        Task<bool> Create(JobTitleChangeClassification item);
        Task<bool> Edit(JobTitleChangeClassification item);
        Task<bool> Delete(JobTitleChangeClassification item);

    }
}
