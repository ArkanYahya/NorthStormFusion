using NorthStorm.Models;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IEvaluation
    {
        public string GetErrors();

        Task<PaginatedList<Evaluation>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Evaluation> GetItem(int id); // read particular item

        Task<bool> Create(Evaluation item);
        Task<bool> Edit(Evaluation item);
        Task<bool> Delete(Evaluation item);
        Task<bool> Update(Evaluation item);

    }
}
