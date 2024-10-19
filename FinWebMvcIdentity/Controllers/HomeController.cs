using FinWebMvcIdentity.Models;
using FinWebMvcIdentity.Models.ViewModel;
using FinWebMvcIdentity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinWebMvcIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecordService _recordService;

        public HomeController(ILogger<HomeController> logger, RecordService recordService)
        {
            _logger = logger;
            _recordService = recordService;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var expenseRecordsByCategory = await _recordService.GetExpenseRecordsByCategoryAsync(userName);
            var incomeRecordsByCategory = await _recordService.GetIncomeRecordsByCategoryAsync(userName);
            var recordValues = await _recordService.GetRecordsValuesAsync(userName);

            var viewModel = new HomeViewModel
            {
                ExpenseRecordsByCategory = expenseRecordsByCategory,
                IncomeRecordsByCategory = incomeRecordsByCategory,
                RecordValues = recordValues
            };
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
