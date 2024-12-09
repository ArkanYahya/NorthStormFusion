using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IJobPosition
    {
        public string GetErrors();

        Task<PaginatedList<JobPosition>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<JobPosition> GetItem(int id); // read particular item

        Task<bool> Create(JobPosition item);
        Task<bool> Edit(JobPosition item);
        Task<bool> Delete(JobPosition item);
        Task<bool> Update(JobPosition item);

    }
}
