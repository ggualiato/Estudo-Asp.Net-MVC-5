using System.Web.Mvc;
using System.Net;

using Modelo.Tabelas;

using Servicos.Tabelas;

namespace Projeto01.Controllers
{
    public class CategoriasController : Controller
    {
        CategoriaServico categoriaServico = new CategoriaServico();

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = categoriaServico.ObterCategoriaPorId((long)id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);
        }

        private ActionResult GravarCategoria(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoriaServico.GravarCategoria(categoria);

                    return RedirectToAction("Index");
                }

                return View(categoria);
            }
            catch
            {
                return View(categoria);
            }            
        }
        
        // GET: Categorias
        public ActionResult Index()
        {
            var categorias = categoriaServico.ObterCategoriasClassificadasPorNome();

            return View(categorias);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {                      
            return GravarCategoria(categoria);
        }

        //Get: Edit
        public ActionResult Edit(long? id)
        {          
            return ObterVisaoFabricantePorId(id);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {                        
            return GravarCategoria(categoria);
        }

        //GET: Details
        public ActionResult Details(long? id)
        {            
            return ObterVisaoFabricantePorId(id);            
        }

        //GET: Delete
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
                Categoria categoria = categoriaServico.EliminarCategoriaPorId(id);

                TempData["Message"] = "Categoria" + categoria.Nome.ToUpper() + "foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}