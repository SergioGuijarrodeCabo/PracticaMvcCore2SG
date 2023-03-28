using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2SG.Models;
using  PracticaMvcCore2SG.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2SG.Controllers
{
    public class ManagedController : Controller
    {

        private RepositoryLibros repo;

        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(string Email, string Pass)
        {

            Usuario user = await this.repo.LoginUserAsync(Email, Pass);

            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name,
                    ClaimTypes.Role

                );
                //Claim clamNombre = new Claim("NOMBRE", usuario.Nombre.ToString());
                Claim claimName = new Claim(ClaimTypes.Name, Email);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                //Claim claimRole = new Claim(ClaimTypes.Role, user.);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                //identity.AddClaim(claimRole);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string Name, string Apellidos, string Email, string Pass, string Foto)
        {
            await this.repo.RegisterUserAsync(Name, Apellidos, Email, Pass, Foto);
            return RedirectToAction("Login");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
