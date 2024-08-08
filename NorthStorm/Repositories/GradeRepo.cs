using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using NorthStorm.Interfaces;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.ViewModels;

namespace NorthStorm.Repositories
{
    public class GradeRepo : IGrade
	{
		private string _errors = "";

		public string GetErrors()
		{
			return _errors;
		}


		private readonly NorthStormContext _context; // for connecting to efcore.
		public GradeRepo(NorthStormContext context) // will be passed by dependency injection.
		{
			_context = context;
		}


		public async Task<bool> Create(Grade grade)
		{
			_errors = "";

			try
			{
				_context.Grades.Add(grade);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_errors = "Create Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		public async Task<bool> Delete(Grade grade)
		{
			_errors = "";

			try
			{
				_context.Attach(grade);
				_context.Entry(grade).State = EntityState.Deleted;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_errors = "Delete Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		public async Task<bool> Edit(Grade grade)
		{
			_errors = "";

			try
			{
				_context.Attach(grade);
				_context.Entry(grade).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_errors = "Update Failed - Sql Exception Occured , Error Info : " + ex.Message + ex.InnerException.Message;
			}
			return false;
		}


		private List<Grade> DoSort(List<Grade> items, string SortProperty, SortOrder sortOrder)
		{
			switch (SortProperty)
			{
				case "GradeNumber":
					if (sortOrder == SortOrder.Ascending)
						items = items.OrderBy(n => n.GradeNumber).ToList();
					else
						items = items.OrderByDescending(n => n.GradeNumber).ToList();
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

		public async Task<PaginatedList<Grade>> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
		{
			List<Grade> items;

			if (!String.IsNullOrEmpty(SearchText))
			{
				items = await _context.Grades.Where(s =>
				s.Id.ToString().Equals(SearchText) ||
				s.GradeNumber.ToString().Equals(SearchText))
					.AsNoTracking()
					.ToListAsync();
			}
			else
			{
				items = await _context.Grades
					.AsNoTracking()
					.ToListAsync();
			}

			items = DoSort(items, SortProperty, sortOrder);

			PaginatedList<Grade> retItems = new PaginatedList<Grade>(items, pageIndex, pageSize);

			return retItems;
		}


		public async Task<Grade> GetItem(int Id)
		{
			Grade item = await _context.Grades
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.Id == Id);
			return item;
		}


	}
}
