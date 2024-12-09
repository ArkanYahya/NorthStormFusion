using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IPunishmentClassification
    {
        public string GetErrors();

        Task<PaginatedList<PunishmentClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<PunishmentClassification> GetItem(int id); // read particular item

        Task<bool> Create(PunishmentClassification item);
        Task<bool> Edit(PunishmentClassification item);
        Task<bool> Delete(PunishmentClassification item);

    }
}
