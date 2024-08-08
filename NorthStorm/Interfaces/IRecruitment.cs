using NorthStorm.Models;
using NorthStorm.Models.ViewModels;
using NorthStorm.ViewModels;

namespace NorthStorm.Interfaces
{
    public interface IRecruitment
    {
        public string GetErrors();

        Task<PaginatedList<Recruitment>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<Recruitment> GetItem(int id); // read particular item

        Task<bool> Create(Recruitment item);
        Task<bool> Edit(Recruitment item);
        Task<bool> Delete(Recruitment item);
        Task<bool> Update(Recruitment item);
        Task<bool> UpdateView(Recruitment item, RecruitmentVM itemViewModel);

    }
}
