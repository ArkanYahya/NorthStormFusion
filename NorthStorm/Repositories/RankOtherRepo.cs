using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class RankOtherRepo : IRankOther
	{
		private string _errors = "";

		public string GetErrors()
		{
			return _errors;
		}


		private readonly NorthStormContext _context; // for connecting to efcore.
		public RankOtherRepo(NorthStormContext context) // will be passed by dependency injection.
		{
			_context = context;
		}


		public async Task<bool> Create(RankOther rankOther)
		{
			_errors = "";

			try
			{
				_context.RankOthers.Add(rankOther);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		public async Task<bool> Delete(RankOther rankOther)
		{
			_errors = "";

			try
			{
				_context.Attach(rankOther);
				_context.Entry(rankOther).State = EntityState.Deleted;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		public async Task<bool> Edit(RankOther rankOther)
		{
			_errors = "";

			try
			{
				_context.Attach(rankOther);
				_context.Entry(rankOther).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		private List<RankOther> DoSort(List<RankOther> items, string SortProperty, SortOrder sortOrder)
		{
			switch (SortProperty)
			{
				case "RankOtherNumber":
					if (sortOrder == SortOrder.Ascending)
						items = items.OrderBy(n => n.RankAsWriting).ToList();
					else
						items = items.OrderByDescending(n => n.RankAsWriting).ToList();
					break;
				default:
					if (sortOrder == SortOrder.Descending)
						items = items.OrderByDescending(d => d.Id).ToList();
					else
						items = items.OrderBy(d => d.Id).ToList();
					break;
			}

			return items;
		}

		public async Task<PaginatedList<RankOther>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
		{
			List<RankOther> items;

			if (!String.IsNullOrEmpty(SearchText))
			{
				items = await _context.RankOthers.Where(s =>
				s.Id.ToString().Equals(SearchText) ||
				s.RankAsWriting.ToString().Equals(SearchText))
					.AsNoTracking()
					.ToListAsync();
			}
			else
			{
				items = await _context.RankOthers
					.AsNoTracking()
					.ToListAsync();
			}

			items = DoSort(items, SortProperty, sortOrder);

			PaginatedList<RankOther> retItems = new PaginatedList<RankOther>(items, pageIndex, pageSize);

			return retItems;
		}


		public async Task<RankOther> GetItem(int Id)
		{
			RankOther item = await _context.RankOthers
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.Id == Id);
			return item;
		}


	}
}
