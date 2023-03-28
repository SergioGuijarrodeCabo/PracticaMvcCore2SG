using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2SG.Models;
using PracticaMvcCore2SG.Repositories;

namespace PracticaMvcCore2SG.ViewComponents
{


    public class MenuGenerosViewComponent : ViewComponent
    {
        public RepositoryLibros repo { get; set; }

        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;

        }

        public async Task<IViewComponentResult> InvokeAsync()

        {
            List<Genero> generos = this.repo.GetGeneros();
            return View(generos);

        }
    }
}
