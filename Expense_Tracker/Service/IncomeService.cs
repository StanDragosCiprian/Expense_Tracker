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

        public Income AddIncomes(Income income)
        {
    
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
