using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface ICareerClassification
    {
        public string GetErrors();

        Task<PaginatedList<CareerClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<CareerClassification> GetItem(int id); // read particular item

        Task<bool> Create(CareerClassification item);
        Task<bool> Edit(CareerClassification item);
        Task<bool> Delete(CareerClassification item);

    }
}
