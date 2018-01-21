using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repository.Entities;

namespace Repository.Context
{
    public class AuthContext : DbContext
    {
        public AuthContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductContext, Repository.Migrations.Configuration>("DefaultConnection"));
            Database.SetInitializer<AuthContext>(null);
        }

        public DbSet<Expense> Expense { set; get; }
        public DbSet<Category> Category { set; get; }
        public DbSet<Bank> Bank { set; get; }

        public DbSet<Supplier> Supplier { set; get; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Activity>()
            //            .HasRequired<Product>(s => s.Products)
            //            .WithMany(s => s.Activities)
            //            .HasForeignKey(s => s.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
