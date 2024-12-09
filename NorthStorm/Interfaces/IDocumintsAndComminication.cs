using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IDocumintsAndComminication
    {
        public string GetErrors();

        Task<PaginatedList<DocumintsAndComminication>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<DocumintsAndComminication> GetItem(int id); // read particular item

        Task<bool> Create(DocumintsAndComminication item);
        Task<bool> Edit(DocumintsAndComminication item);
        Task<bool> Delete(DocumintsAndComminication item);

    }
}
