using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Persistencia.Contexts;

using Modelo.Tabelas;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            return context.Categorias.OrderBy(b => b.Nome);
        }
    }
}
