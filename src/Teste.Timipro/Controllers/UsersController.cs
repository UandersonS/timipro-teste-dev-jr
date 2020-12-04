using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Teste.Timipro.Models;

namespace Teste.Timipro.Controllers
{
    public class UsersController : Controller
    {
        private TimiproContext db = new TimiproContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Product);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var products = db.Products.Where(p => p.Active && !p.Users.Any());
            

            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName");
            return View();
        }

        // POST: Users/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,FirstName,CPF,ProductId")] User user)
        {
            if (db.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Este E-Mail já foi cadastrado");
            }

            if (db.Users.Any(u => u.CPF == user.CPF))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName", user.ProductId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            var products = db.Products.Where(p => p.Active && !p.Users.Any());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName", user.ProductId);
            return View(user);
        }

        // POST: Users/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,FirstName,CPF,ProductId")] User user)
        {

            if (db.Users.Any(u => u.UserName == user.UserName && u.UserId != user.UserId))
            {
                ModelState.AddModelError("UserName", "Este E-Mail ja foi cadastrado");
            }
            if (db.Users.Any(u => u.CPF == user.CPF && u.UserId != user.UserId))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var products = db.Products.Where(p => p.Active && !p.Users.Any());
            ViewBag.ProductId = new SelectList(products, "ProductID", "ProductName", user.ProductId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
