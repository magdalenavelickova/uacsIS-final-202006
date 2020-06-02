using System.Diagnostics;
using System.Threading.Tasks;

using BankApplication.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using UniversityApplication.WebApi.Models;

namespace BankApplication.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankDataContext _dbContext;

        public HomeController(ILogger<HomeController> logger, BankDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
          

            return View();
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
