using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IEducationalInstitution
    {
        public string GetErrors();

        Task<PaginatedList<EducationalInstitution>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<EducationalInstitution> GetItem(int id); // read particular item

        Task<bool> Create(EducationalInstitution item);
        Task<bool> Edit(EducationalInstitution item);
        Task<bool> Delete(EducationalInstitution item);
    }
}
