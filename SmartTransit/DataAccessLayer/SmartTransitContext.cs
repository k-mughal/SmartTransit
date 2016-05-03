using SmartTransit.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;


namespace SmartTransit.DataAccessLayer
{
    public class SmartTransitContext: DbContext
    {

        public SmartTransitContext() : base("SmartTransitContext")
        {
            //  base.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<SmartTransitContext>(new DropCreateDatabaseIfModelChanges<SmartTransitContext>());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<LogHistory> LogsHistory { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.Configuration.ProxyCreationEnabled = false;
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}