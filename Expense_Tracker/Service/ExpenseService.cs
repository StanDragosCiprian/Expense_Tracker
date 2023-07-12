using Expense_Tracker.Data;
using Expense_Tracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Service
{
	public class ExpenseService
	{
		private readonly ExpenseTrackerContext _context;

		public ExpenseService(ExpenseTrackerContext context)
		{
			_context = context;
		}

		public List<Expense> GetExpenses()
		{
			return _context.Expenses.ToList();
		}
        public Expense AddExpensesy(Expense expense)
        {
			expense.Date = expense.Date.ToUniversalTime();
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return expense;
        }
		public List<Expense> GetExpensesByCategory(int id)
		{
			return _context.Expenses.Where(expense => expense.CategoryId == id).AsNoTracking().ToList();
		}
		public Expense getOneById(int id)
		{
			try
			{
                Expense? expenseItem = _context.Expenses.Find(id);
                if (expenseItem != null)
				{
					return expenseItem;
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
        public List<Expense> update(Expense expense)
		{
            expense.Date = expense.Date.ToUniversalTime();
            _context.Expenses.Entry(expense).State = EntityState.Modified;
					_context.SaveChanges();
			
			return _context.Expenses.ToList();
		}

		public void DeleteExpense(int id)
		{
			try
			{
				Expense? expenses = _context.Expenses.Find(id);
				if (expenses != null)
				{
					_context.Expenses.Remove(expenses);
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
