using Expense_Tracker.Data;
using Expense_Tracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Eventing.Reader;

namespace Expense_Tracker.Service
{
    public class ExpenseService
    {
        private readonly ExpenseTrackerContext _context;

        public ExpenseService(ExpenseTrackerContext context)
        {
            _context = context;
        }
        public int sum()
        {
            return _context.Expenses.Sum(i => i.Amount);
        }
        public DateOnly minDate()
        {
            DateOnly dateOnly = DateOnly.FromDateTime(_context.Expenses.Min(i => i.Date));
            return dateOnly;
        }
		public int maxCategory()
		{
			return _context.Expenses.OrderByDescending(e => e.Amount).FirstOrDefault().CategoryId;
		}

		public int currentMothSum()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            int sum = _context.Expenses
                .Where(i => i.Date.Month == currentMonth && i.Date.Year == currentYear)
                .Sum(i => i.Amount);

            return sum;
        }
        public int planed()
        {
            int sum = _context.Expenses
                .Where(i => i.Planned == true)
                .Sum(i => i.Amount);


            return sum;
        }
        public DateOnly maxDate()
        {
            DateOnly dateOnly = DateOnly.FromDateTime(_context.Expenses.Max(i => i.Date));
            return dateOnly;
        }
        public List<Expense> GetExpenses()
        {
            return _context.Expenses.ToList();
        }
        public List<Expense> getPlanned(string plande)
        {
            if (plande == "Plannes")
            {
                return _context.Expenses.Where(i => i.Planned).ToList();
            }
            else if (plande == "UnPlannes")
            {
                return _context.Expenses.Where(i => !i.Planned).ToList();
            }
            else if (plande == "All plan")
            {
                return _context.Expenses.ToList();
            }
            else
            {
                // Handle invalid input
                return new List<Expense>();
            }
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
