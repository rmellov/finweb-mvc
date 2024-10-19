using FinWebMvcIdentity.Data;
using Microsoft.EntityFrameworkCore;

namespace FinWebMvcIdentity.Services
{
    public class RecordService
    {
        private readonly ApplicationDbContext _context;

        public RecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDictionary<string, decimal>> GetExpenseRecordsByCategoryAsync(string userName)
        {
            var records = await _context.Records
                .Include(r => r.Category)
                .Where(r => r.Type == Enums.EType.Expense && r.User == userName)
                .GroupBy(r => r.Category.Description)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(d => d.Value));                

            return records;
        }

        public async Task<IDictionary<string, decimal>> GetIncomeRecordsByCategoryAsync(string userName)
        {
            var records = await _context.Records
                .Include(r => r.Category)
                .Where(r => r.Type == Enums.EType.Income && r.User == userName)
                .GroupBy(r => r.Category.Description)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(d => d.Value));

            return records;
        }

        public async Task<IDictionary<string, decimal>> GetRecordsValuesAsync(string userName)
        {
            var pendingExpenses = await _context.Records
                .Where(l => l.Status == Enums.EStatus.Pending && l.Type == Enums.EType.Expense && l.User == userName)
                .SumAsync(l => (double)l.Value);

            var expenses = await _context.Records
                .Where(l => l.Status == Enums.EStatus.Complete && l.Type == Enums.EType.Expense && l.User == userName)
                .SumAsync(l => (double)l.Value);

            var pendingIncomes = await _context.Records
                .Where(l => l.Status == Enums.EStatus.Pending && l.Type == Enums.EType.Income && l.User == userName)
                .SumAsync(l => (double)l.Value);

            var incomes = await _context.Records
                .Where(l => l.Status == Enums.EStatus.Complete && l.Type == Enums.EType.Income && l.User == userName)
                .SumAsync(l => (double)l.Value);

            var total = (decimal)incomes - (decimal)expenses;

            return new Dictionary<string, decimal>
            {
                { "PendingExpenses", (decimal)pendingExpenses },
                { "Expenses", (decimal)expenses },
                { "PendingIncomes", (decimal)pendingIncomes },
                { "Incomes", (decimal)incomes },
                { "Total", total }
            };
        }


    }
}
