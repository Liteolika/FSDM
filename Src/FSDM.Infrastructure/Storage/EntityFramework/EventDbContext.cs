using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDM.Infrastructure.Storage.EntityFramework
{
    public class EventDbContext : DbContext
    {

        public EventDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public DbSet<EventData> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public static EventDbContext Create(string nameOrConnectionString)
        {
            return new EventDbContext(nameOrConnectionString);
        }

    }
}
