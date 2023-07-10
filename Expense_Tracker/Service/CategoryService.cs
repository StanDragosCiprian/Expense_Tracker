using Expense_Tracker.Data;
using Expense_Tracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Service
{
    public class CategoryService
    {
        private readonly ExpenseTrackerContext _context;

        public CategoryService(ExpenseTrackerContext context)
        {
            _context = context;
        }
		
		public List<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}
		public Category AddCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();

			return category;
		}
		
		public void DeleteCategory(int id)
		{
			try
			{
				Category? category = _context.Categories.Find(id);
				if (category != null)
				{
					_context.Categories.Remove(category);
					_context.SaveChanges();
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch
			{
				throw;
			}
		}
	}


}

