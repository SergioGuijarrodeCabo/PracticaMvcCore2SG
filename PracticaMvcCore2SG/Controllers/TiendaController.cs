using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2SG.Extensions;
using PracticaMvcCore2SG.Filters;
using PracticaMvcCore2SG.Models;
using PracticaMvcCore2SG.Repositories;
using System.Security.Claims;

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

        public async Task<IActionResult> Detalles(int idLibro)
        {
            Libro libro = await this.repo.GetLibro(idLibro);
            return View(libro);

        }



       
        public async Task<IActionResult> Carrito(int cantidadItem)
        {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Libro> libros = new List<Libro>();
            if (listidproducts != null)
            {
                foreach (int id in listidproducts)
                {
                    Libro libro = await this.repo.GetLibro(id);
                    libros.Add(libro);
                }
            }
            ViewBag.CANTIDAD = cantidadItem;
            return View(libros);
        }

        //[AuthorizeUsers]
        public IActionResult AgregarCarrito(int idLibro)
        {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");

            //COMPROBAR SI CARRITO ESTA EN SESSION
            if (listidproducts != null)
            {
                listidproducts.Add(idLibro);
                HttpContext.Session.SetObject("CARRITO", listidproducts);
            }
            else
            {
                //CREAR CARRITO EN SESSION
                List<int> Nueva_listidproducts = new List<int> { idLibro };
                HttpContext.Session.SetObject("CARRITO", Nueva_listidproducts);
            }
            return RedirectToAction("Index");
        }


        public IActionResult FinalizarCompra()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarCompra(string idusuario, [FromBody] Libro[] libros)
        {

            int i = 0;
           
            //for (int i = 0; i < libros.Length; i++)
            //{
            //    Pedido pedido = new Pedido();
            //    pedido.IdPedido = await this.repo.MaxIdPedido();
            //    pedido.IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //    pedido.Fecha = DateTime.Today;
            //    pedido.IdLibro = idlibros[i];
            //    pedido.Cantidad = cantidades[i];
            //    await this.repo.CrearPedido(pedido);
            //}


            return RedirectToAction("VistaPedidos");
        }

        public async Task<IActionResult> VistaPedidos()
        {
            List<Pedido> pedidos = await this.repo.GetPedidos();
            return View(pedidos);
        }

        
    }
}
