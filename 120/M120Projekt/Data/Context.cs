using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace M120Projekt.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=M120Connectionstring")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.Context, M120Projekt.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>().ToTable("Library"); // Damit kein "s" angehängt wird an Tabelle

            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            //fixes that damn error
        }
        public DbSet<Library> Library { get; set; }
    }
}
