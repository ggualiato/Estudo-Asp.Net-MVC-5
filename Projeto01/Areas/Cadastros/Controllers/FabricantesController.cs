using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

using Modelo.Cadastros;
using System.Net;

using Servicos.Cadastros;
using Servicos.Tabelas;

namespace Projeto01.Areas.Cadastros
{
    public class FabricantesController : Controller
    {
        private FabricanteServico fabricanteServico = new FabricanteServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private ProdutoServico produtoServico = new ProdutoServico();

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = fabricanteServico.ObterFabricantePorId((long)id);

            if (fabricante == null)
            {
                return HttpNotFound();
            }

            return View(fabricante);

        }

        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fabricanteServico.GravarFabricante(fabricante);

                    return RedirectToAction("Index");
                }
            
                return View(fabricante);
            }
            catch
            {
                return View(fabricante);
            }
        }

        // GET: Fabricantes
        public ActionResult Index()
        {
            var fabricantes = fabricanteServico.ObterFabricantesClassificadosPorNome();

            return View(fabricantes);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {            
            return GravarFabricante(fabricante);
        }

        // GET: Edit
        public ActionResult Edit(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {            
            return GravarFabricante(fabricante);
        }

        // GET: Details
        public ActionResult Details(long? id)
        {            
            return ObterVisaoFabricantePorId(id);            
        }

        // GET: Delete
        public ActionResult Delete(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Fabricante fabricante = fabricanteServico.EliminarFabricantePorId(id);

                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }            
        }
    }
}