using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Collections.Generic;

using Modelo.Cadastros;
using Servicos.Cadastros;
using Servicos.Tabelas;

namespace Projeto01.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((long)id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);

                    return RedirectToAction("Index");
                }

                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }

        // GET: Produtos
        public ActionResult Index()
        {            
            var produtos = produtoServico.ObterProdutosClassificadosPorNome();

            return View(produtos);
        }
        
        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {            
            return ObterVisaoProdutoPorId(id);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();

            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));     
            
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Produto produto)
        {
            return GravarProduto(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);

                TempData["Message"] = "Produto" + produto.Nome.ToUpper() + "foi removido";
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
