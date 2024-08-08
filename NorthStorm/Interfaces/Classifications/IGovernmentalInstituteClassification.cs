﻿using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models;
using NorthStorm.Models.Classifications;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Interfaces.Classifications
{
    public interface IGovernmentalInstituteClassification
    {
        public string GetErrors();

        Task<PaginatedList<GovernmentalInstituteClassification>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5);
        Task<GovernmentalInstituteClassification> GetItem(int id); // read particular item

        Task<bool> Create(GovernmentalInstituteClassification item);
        Task<bool> Edit(GovernmentalInstituteClassification item);
        Task<bool> Delete(GovernmentalInstituteClassification item);

    }
}
