using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IEvaluationClassification
    {
        public string GetErrors();

        Task<PaginatedList<EvaluationClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<EvaluationClassification> GetItem(int id); // read particular item

        Task<bool> Create(EvaluationClassification item);
        Task<bool> Edit(EvaluationClassification item);
        Task<bool> Delete(EvaluationClassification item);

    }
}
