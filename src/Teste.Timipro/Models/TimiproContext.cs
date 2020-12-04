using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Teste.Timipro.Models
{
    public class TimiproContext : DbContext
    {
        public TimiproContext() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();            
        }

        public System.Data.Entity.DbSet<Teste.Timipro.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Teste.Timipro.Models.User> Users { get; set; }
    }
}