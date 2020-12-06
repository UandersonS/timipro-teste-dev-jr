using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Teste.Timipro.Database;
using Teste.Timipro.Entities;

namespace Teste.Timipro.Business
{
    public class ClientBusiness
    {
        private TimiproContext db = new TimiproContext();
        public List<ClientEntity>GetAll( )
        {
            var clients = db.Clients.Include(c => c.Product);
            return clients.ToList();
        }
        public ClientEntity Get(int id)
        {
            return db.Clients.Find(id);
        }
        public void Save(ClientEntity client)
        {
            if(client.ClientID > 0)
            {
                db.Entry(client).State = EntityState.Modified;
            }
            else
            {
                db.Clients.Add(client);
            }
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            ClientEntity client = Get(id);
            db.Clients.Remove(client);
            db.SaveChanges();
        }
        public bool HasClientWithEmail (string email, int ignoredClientId = 0)
        {
            return db.Clients.Any(u => u.Email == email && u.ClientID != ignoredClientId);
        }
        
        public bool HasClientWithCPF (string cpf, int ignoredClientId = 0)
        {
            return db.Clients.Any(u => u.CPF == cpf && u.ClientID != ignoredClientId);
        }

    }
}
