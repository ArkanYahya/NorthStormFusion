using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Assistants
{
    public interface IStatus
    {
        public string GetErrors();

        Task<PaginatedList<Status>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Status> GetItem(int id); // read particular item

        Task<bool> Create(Status item);
        Task<bool> Edit(Status item);
        Task<bool> Delete(Status item);

    }
}
