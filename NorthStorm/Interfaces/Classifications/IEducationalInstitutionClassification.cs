using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IEducationalInstitutionClassification
    {
        public string GetErrors();

        Task<PaginatedList<EducationalInstitutionClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<EducationalInstitutionClassification> GetItem(int id); // read particular item

        Task<bool> Create(EducationalInstitutionClassification item);
        Task<bool> Edit(EducationalInstitutionClassification item);
        Task<bool> Delete(EducationalInstitutionClassification item);
    }
}
