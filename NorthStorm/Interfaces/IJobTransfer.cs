using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IJobTransfer
    {
        public string GetErrors();

        Task<PaginatedList<JobTransfer>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<JobTransfer> GetItem(int id); // read particular item

        Task<bool> Create(JobTransfer item);
        Task<bool> Edit(JobTransfer item);
        Task<bool> Delete(JobTransfer item);
        Task<bool> Update(JobTransfer item);

    }
}
