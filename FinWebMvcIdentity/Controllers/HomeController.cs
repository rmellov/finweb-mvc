using FinWebMvcIdentity.Models;
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
            var recordsByCategory = await _recordService.GetRecordsByCategoryAsync();
            return View(recordsByCategory);
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
