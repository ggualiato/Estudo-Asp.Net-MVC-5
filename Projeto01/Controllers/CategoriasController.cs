using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

using Projeto01.Contexts;
using Modelo.Tabelas;
using System.Xml.Linq;

namespace Projeto01.Controllers
{
    public class CategoriasController : Controller
    {
        private EFContext context = new EFContext();
        
        // GET: Categorias
        public ActionResult Index()
        {
            return View(context.Categorias.OrderBy(c => c.Nome));
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
            context.Categorias.Add(categoria);
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        //Get: Edit
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View(categoria);
        }

        //GET: Details
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Where(c => c.CategoriaId == id).Include("Produtos.Fabricante").First();

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);            
        }

        //GET: Delete
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = context.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return View(categoria);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = context.Categorias.Find(id);
            
            context.Categorias.Remove(categoria);
            context.SaveChanges();

            TempData["Message"] = "Categoria" + categoria.Nome.ToUpper() + " foi removida";

            return RedirectToAction("Index");
        }


    }
}