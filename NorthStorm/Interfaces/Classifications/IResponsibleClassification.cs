using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IResponsibleClassification
    {
        public string GetErrors();

        Task<PaginatedList<ResponsibleClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<ResponsibleClassification> GetItem(int id); // read particular item

        Task<bool> Create(ResponsibleClassification item);
        Task<bool> Edit(ResponsibleClassification item);
        Task<bool> Delete(ResponsibleClassification item);

    }
}
