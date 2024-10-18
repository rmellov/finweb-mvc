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
    }
}
