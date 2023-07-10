using Expense_Tracker.Data;
using Expense_Tracker.Data.Models;

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
	}
}
