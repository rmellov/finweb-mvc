using FinWebMvcIdentity.Data;
using FinWebMvcIdentity.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinWebMvcIdentity.Services
{
    public class RecordService : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RecordByCategoryViewModel>> GetRecordsByCategoryAsync()
        {
            // TODO CORRIGIR 
            var records = await _context.Records
                .Where(a => a.User == User.Identity.Name)
                .GroupBy(r => r.Category)
                .Select(g => new RecordByCategoryViewModel { Category = g.Key, Total = g.Sum(d => (double)d.Value) })                
                .ToListAsync();

            return records;
        }
    }
}
