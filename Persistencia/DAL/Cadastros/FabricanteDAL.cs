using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Persistencia.Contexts;

using Modelo.Cadastros;

namespace Persistencia.DAL.Cadastros
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(b => b.Nome);
        }

        public Fabricante ObterFabricantePorId(long id)
        {
            return context.Fabricantes.Where(f => f.FabricanteId == id).First();
        }

        public void GravarFabricante(Fabricante fabricante)
        {
            if (fabricante.FabricanteId == null)
            {
                context.Fabricantes.Add(fabricante);
            }
            else
            {
                context.Entry(fabricante).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public Fabricante ElimarFabricantePorId(long id)
        {
            Fabricante fabricante = ObterFabricantePorId(id);

            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();

            return fabricante;
        }
    }
}
