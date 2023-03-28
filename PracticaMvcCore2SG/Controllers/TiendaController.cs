using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2SG.Models;
using PracticaMvcCore2SG.Repositories;

namespace PracticaMvcCore2SG.Controllers
{
    public class TiendaController : Controller
    {
        public RepositoryLibros repo { get; set; }

        public TiendaController(RepositoryLibros repo)
        {
            this.repo = repo;
        }


        public async Task<IActionResult> Index(int idgenero)
        {         
                 
            List<Libro> libros = await this.repo.GetLibros(idgenero);
            return View(libros);
        }

        public async Task<IActionResult> Detalles(int IdLibro)
        {
            Libro libro = await this.repo.GetLibro(IdLibro);
            return View(libro);

        }
    }
}
