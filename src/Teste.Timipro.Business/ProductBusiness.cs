using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Timipro.Database;
using Teste.Timipro.Entities;

namespace Teste.Timipro.Business
{
    public class ProductBusiness
    {
        private TimiproContext db = new TimiproContext();
        public List<ProductEntity> GetActiveProducts(int clientId = 0)
        {
            var products = db.Products.Where(p => p.Active && (!p.Clients.Any() || p.Clients.Any(c => c.ClientID == clientId)));
            return products.ToList();
        }
        public List<ProductEntity> GetAll()
        {
            return db.Products.ToList();
        }
        public ProductEntity Get(int id)
        {
            return db.Products.Find(id);
        }
        public void Save(ProductEntity product)
        {
            if (product.ProductID > 0)
            {
                if (product.Active == false && db.Clients.Any(c => c.ProductId == product.ProductID))
                {
                    throw new Exception("Não é possível desativar um produto com cliente associado");
                }

                db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                db.Products.Add(product);
            }
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            ProductEntity product = Get(id);
            if (product.Clients.Any())
            {
                throw new Exception("Não é possível excluir um produto com cliente associado");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            
        }


    }
}
