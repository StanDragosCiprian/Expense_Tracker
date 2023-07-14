using Expense_Tracker.Data;
using Expense_Tracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
namespace Expense_Tracker.Service
{
    public class IncomeService
    {
        private readonly ExpenseTrackerContext _context;

        public IncomeService(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public List<Income> getIncomes()
        {
            
            return _context.Incomes.ToList();
        }
        public int sum()
        {
            return _context.Incomes.Sum(i => i.Amount);
		}
		public DateOnly minDate()
		{
			DateOnly dateOnly = DateOnly.FromDateTime(_context.Incomes.Min(i => i.Date));
			return dateOnly;
		}
		public DateOnly maxDate()
		{
			DateOnly dateOnly = DateOnly.FromDateTime(_context.Incomes.Max(i => i.Date));
			return dateOnly;
		}
		public int currentMothSum()
		{
			int currentMonth = DateTime.Now.Month;
			int currentYear = DateTime.Now.Year;

			int sum = _context.Incomes
				.Where(i => i.Date.Month == currentMonth && i.Date.Year == currentYear)
				.Sum(i => i.Amount);


			return sum;
		}
		public List<Income> filterByDate(DateTime start, DateTime end)
		{
			DateTime startDate = start.Date.ToUniversalTime();
			DateTime endDate = end.ToUniversalTime();
			return _context.Incomes.Where(i => i.Date >= startDate && i.Date <= endDate).ToList();
		}


		public Income AddIncomes(Income income)
        {
			income.Date = income.Date.ToUniversalTime();
			_context.Incomes.Add(income);
            _context.SaveChanges();

            return income;
        }
		public List<Income> update(Income income)
		{
			income.Date = income.Date.ToUniversalTime();
			_context.Incomes.Entry(income).State = EntityState.Modified;
			_context.SaveChanges();

			return _context.Incomes.ToList();
		}
		public Income getOneById(int id)
		{
			try
			{
				Income? incomItem = _context.Incomes.Find(id);
				if (incomItem != null)
				{
					return incomItem;
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
        public void DeleteIncome(int id)
        {
            try
            {
                Income? income = _context.Incomes.Find(id);
                if (income != null)
                {
                    _context.Incomes.Remove(income);
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
