using PracticaMvcCore2SG.Data;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2SG.Data;
using PracticaMvcCore2SG.Models;

namespace PracticaMvcCore2SG.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;

        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> LoginUserAsync(string Email, string Pass)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                return null;
            }
            else
            {
                 user = await this.context.Usuarios.FirstOrDefaultAsync(u => u.Pass == Pass);
                if (user == null) {

                    return null;
                }
                else
                {

                    return user;
                }
            }
        }


        public async Task RegisterUserAsync(string Name, string Apellidos, string Email, string Pass, string Foto)
        {
            var newid = this.context.Usuarios.Any() ? this.context.Usuarios.Max(u => u.IdUsuario) + 1 : 1;
            Usuario user = new Usuario();
            user.IdUsuario = newid;
            user.Nombre = Name;
            //user.Role = "cliente";
            user.Apellidos = Apellidos;
            user.Email = Email;
            user.Pass = Pass;
            user.Foto = Foto;

            //user.Salt = HelperCryptography.GenerateSalt();
            //user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            //User.AQUISILOVES = password

            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }


        public List<Genero> GetGeneros() {

            //var consulta = this.context.Generos.ToList();
            //List<string> generos = new List<string>();
            //for(int i = 0; i < consulta.Count; i++){
            //    generos.Add(consulta[i].Nombre);
            //}

            var generos = this.context.Generos
                            .ToList();
            //var generos =  this.context.Generos
            //                .Select(g => g.Nombre)
            //                .Distinct()
            //                .ToList();
            return generos;
        }

        public async Task<List<Libro>> GetTodosLibros(int posicion)
        {
            List<Libro> libros = await this.context.Libros.ToListAsync();
            return libros.Skip(posicion).Take(5).ToList();

        }

        public Task<int> numLibros()
        {
            return this.context.Libros.CountAsync();
        }


        public async Task<List<Libro>> GetLibros(int idGenero)
        {
            var consulta = await this.context.Libros
                .Where(libro => libro.IdGenero == idGenero)
                .ToListAsync();

            return consulta;
        }

        public async Task<Libro> GetLibro(int idLibro)
        {
            var consulta = await this.context.Libros
                .Where(libro => libro.IdLibro == idLibro)
                .FirstOrDefaultAsync();

            return consulta;
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            var consulta = await this.context.Pedidos.ToListAsync();
            return consulta;
        }

        public async Task CrearPedido(Pedido pedido)
        {
            this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> MaxIdPedido()
        {
            int maxId = await this.context.Pedidos.MaxAsync(p => p.IdPedido);
            return maxId + 1;
        }

    }
}
