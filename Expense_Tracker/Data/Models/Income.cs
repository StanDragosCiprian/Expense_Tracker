namespace Expense_Tracker.Data.Models
{
	public class Income
	{
       public int Id { set; get; }
       public string Title { set; get; }
        public DateTime Date { set; get; }
        public int Amount { set; get; }
       public Type type { set; get; }   
    }
    public enum Type
    {
        Salary,
        Scholarship,
        Gift,
        LuckyWinnings
    }
}
