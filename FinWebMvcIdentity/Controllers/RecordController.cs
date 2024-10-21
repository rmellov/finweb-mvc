using FinWebMvcIdentity.Data;
using FinWebMvcIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace FinWebMvcIdentity.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, DateTime? minDate, DateTime? maxDate)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = _context.Records
                .AsNoTracking()
                .Include(c => c.Category)
                .Where(r => r.User == User.Identity.Name);

            if (minDate.HasValue)
            {
                query = query.Where(d => d.RegisterDate >= minDate.Value);
                ViewBag.MinDate = minDate;
            };

            if (maxDate.HasValue)
            {
                query = query.Where(d => d.RegisterDate <= maxDate.Value);
                ViewBag.MaxDate = maxDate;
            };

            query = query.OrderByDescending(d => d.RegisterDate);

            var applicationDbContext = await query.ToListAsync();
            return View(applicationDbContext.ToPagedList(pageNumber, pageSize));
        }        

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(u => u.User == User.Identity.Name), "Id", "Description");

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,CategoryId,Description,RegisterDate,Value,MaturityPaymentDate,Status")] Record @record)
        {
            if (ModelState.IsValid)
            {
                @record.User = User.Identity.Name;
                _context.Add(@record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", @record.CategoryId);
            return View(@record);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records.FindAsync(id);

            if (!ValidUser(@record))
            {
                return NotFound();
            }

            if (@record == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(u => u.User == User.Identity.Name), "Id", "Description", @record.CategoryId);
            return View(@record);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,CategoryId,Description,RegisterDate,Value,MaturityPaymentDate,Status")] Record @record)
        {
            if (id != @record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    @record.User = User.Identity.Name;                    
                    _context.Update(@record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@record.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", @record.CategoryId);
            return View(@record);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(c => c.Category)
                .Where(u => u.User == User.Identity.Name)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@record == null)
            {
                return NotFound();
            }

            if (!ValidUser(@record))
            {
                return NotFound();
            }

            return View(@record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @record = await _context.Records.FindAsync(id);

            if (@record != null)
            {
                _context.Records.Remove(@record);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }

        private bool ValidUser(Record @record)
        {
            return _context.Records.Any(u => u.User == User.Identity.Name);
        }
    }
}
