using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2SG.Models;
using PracticaMvcCore2SG.Repositories;
using System.Diagnostics;

namespace PracticaMvcCore2SG.Controllers
{
    public class HomeController : Controller
    {
        public RepositoryLibros repo {get;set;}


       

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RepositoryLibros repo)
        {
            this.repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
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