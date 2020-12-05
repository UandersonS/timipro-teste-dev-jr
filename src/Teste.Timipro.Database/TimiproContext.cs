using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Teste.Timipro.Entities;

namespace Teste.Timipro.Database
{
    public class TimiproContext : DbContext
    {
        public TimiproContext() : base("TimiproDatabase")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
    }
}
