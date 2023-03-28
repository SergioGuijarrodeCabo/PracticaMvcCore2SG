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

        public async Task<IActionResult> Index(int? posicion)
        {

            int numLibros = await this.repo.numLibros();
            if(posicion == null)
            {
                posicion = 0;
            }
            if(posicion > numLibros)
            {
                posicion = 0;

            }
            if (numLibros < 0)
            {
                posicion = numLibros + 1;
            }

            List <Libro> libros = await this.repo.GetTodosLibros(posicion.Value);
            ViewData["SIGUIENTE"] = posicion + 4;
            ViewData["ANTERIOR"] = posicion - 4;
            return View(libros);
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