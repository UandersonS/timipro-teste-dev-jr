using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Teste.Timipro.Database;
using Teste.Timipro.Entities;

namespace Teste.Timipro.Web.Controllers
{
    public class ClientsController : Controller
    {
        private TimiproContext db = new TimiproContext();
        // GET: clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Product);
            return View(clients.ToList());
        }

        // GET: clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntity client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: clients/Create
        public ActionResult Create()
        {
            var products = db.Products.Where(p => p.Active && !p.Clients.Any());


            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName");
            return View();
        }

        // POST: clients/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,Name,Email,CPF,ProductId")] ClientEntity client)
        {
            if (db.Clients.Any(u => u.Email == client.Email))
            {
                ModelState.AddModelError("Email", "Este E-Mail já foi cadastrado");
            }

            if (db.Clients.Any(u => u.CPF == client.CPF))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var products = db.Products.Where(p => p.Active && !p.Clients.Any());
            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // GET: clients/Edit/5
        public ActionResult Edit(int? id)
        {
            var products = db.Products.Where(p => p.Active && !p.Clients.Any());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntity client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // POST: clients/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,Name,Email,CPF,ProductId")] ClientEntity client)
        {

            if (db.Clients.Any(u => u.Email == client.Email && u.ClientID != client.ClientID))
            {
                ModelState.AddModelError("Email", "Este E-Mail ja foi cadastrado");
            }
            if (db.Clients.Any(u => u.CPF == client.CPF && u.ClientID != client.ClientID))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var products = db.Products.Where(p => p.Active && !p.Clients.Any());
            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // GET: clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntity client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            ClientEntity client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}