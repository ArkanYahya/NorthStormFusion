using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface ICertificate
    {
        public string GetErrors();

        Task<PaginatedList<Certificate>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Certificate> GetItem(int id); // read particular item

        Task<bool> Create(Certificate item);
        Task<bool> Edit(Certificate item);
        Task<bool> Delete(Certificate item);

    }
}
