namespace FinWebMvcIdentity.Models.ViewModel
{
    public class HomeViewModel
    {
        public IDictionary<string, decimal> ExpenseRecordsByCategory { get; set; }
        public IDictionary<string, decimal> IncomeRecordsByCategory { get; set; }
        public IDictionary<string, decimal> RecordValues { get; set; }
    }
}
