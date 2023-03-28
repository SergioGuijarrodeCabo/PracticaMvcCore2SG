using PracticaMvcCore2SG.Data;

namespace PracticaMvcCore2SG.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;

        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }


    }
}
