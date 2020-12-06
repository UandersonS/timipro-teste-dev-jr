using System.Net;
using System.Web.Mvc;
using Teste.Timipro.Business;
using Teste.Timipro.Entities;

namespace Teste.Timipro.Web.Controllers
{
    public class ClientsController : Controller
    {
        ClientBusiness clientBusiness = new ClientBusiness();
        ProductBusiness productBusiness = new ProductBusiness();

        // GET: clients
        public ActionResult Index()
        {
            var clients = clientBusiness.GetAll();
            return View(clients);
        }

        // GET: clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientEntity client = clientBusiness.Get(id.Value);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: clients/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(productBusiness.GetActiveProducts(), "ProductID", "ProductName");
            return View();
        }

        // POST: clients/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,Name,Email,CPF,ProductId")] ClientEntity client)
        {
            if (clientBusiness.HasClientWithEmail(client.Email))
            {
                ModelState.AddModelError("Email", "Este E-Mail já foi cadastrado");
            }
            if (clientBusiness.HasClientWithCPF(client.CPF))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }

            if (ModelState.IsValid)
            {
                clientBusiness.Save(client);
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(productBusiness.GetActiveProducts(), "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // GET: clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntity client = clientBusiness.Get(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(productBusiness.GetActiveProducts(client.ClientID), "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // POST: clients/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,Name,Email,CPF,ProductId")] ClientEntity client)
        {

            if (clientBusiness.HasClientWithEmail(client.Email, client.ClientID))
            {
                ModelState.AddModelError("Email", "Este E-Mail ja foi cadastrado");
            }
            if (clientBusiness.HasClientWithCPF(client.CPF, client.ClientID))
            {
                ModelState.AddModelError("CPF", "Este CPF já foi cadastrado");
            }

            if (ModelState.IsValid)
            {
                clientBusiness.Save(client);
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(productBusiness.GetActiveProducts(client.ClientID), "ProductID", "ProductName", client.ProductId);
            return View(client);
        }

        // GET: clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientEntity client = clientBusiness.Get(id.Value);

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
            clientBusiness.Delete(id);
            return RedirectToAction("Index");
        }

    }
}