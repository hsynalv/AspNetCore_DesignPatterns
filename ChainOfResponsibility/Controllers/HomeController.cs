using ChainOfResponsibility.COf_Responsibility;
using ChainOfResponsibility.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChainOfResponsibility.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext appIdentityDbContext)
        {
            _logger = logger;
            _context = appIdentityDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendEmail()
        {
            var products = await _context.Products.ToListAsync();

            var excelProcessHandler = new ExcelProcessHandler<Product>();

            var zipFileProcessHandler = new ZipFileProcessHandler<Product>();

            var sendEmailProcessHandler = new SendEmailProcessHandler("products.zip","alavhasan72892@gmail.com");

            excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);
            excelProcessHandler.Handler(products);

            return View(nameof(Index));
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
