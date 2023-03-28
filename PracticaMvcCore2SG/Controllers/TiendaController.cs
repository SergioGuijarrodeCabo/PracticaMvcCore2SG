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



       
        public async Task<IActionResult> Carrito(int cantidadItem)
        {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Libro> items = new List<Libro>();
            if (listidproducts != null)
            {
                foreach (int id in listidproducts)
                {
                    Item item = await this.repo.FindItemAsync(id);
                    items.Add(item);
                }
            }
            ViewBag.CANTIDAD = cantidadItem;
            return View(items);
        }


        public IActionResult AgregarCarrito(int idProduct)
        {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");

            //COMPROBAR SI CARRITO ESTA EN SESSION
            if (listidproducts != null)
            {
                listidproducts.Add(idProduct);
                HttpContext.Session.SetObject("CARRITO", listidproducts);
            }
            else
            {
                //CREAR CARRITO EN SESSION
                List<int> Nueva_listidproducts = new List<int> { idProduct };
                HttpContext.Session.SetObject("CARRITO", Nueva_listidproducts);
            }
            return RedirectToAction("Index");
        }
    }
}
