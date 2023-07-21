using Assigment2Api.DBOperations.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assigment2Api.DBOperations.DBContext
{
    public class SimDbContext : DbContext
    {
        public SimDbContext(DbContextOptions<SimDbContext> options) : base(options)
        {

        }


        // dbset

        public DbSet<Transaction> Transaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            base.OnModelCreating(modelBuilder);
        }



    }
}
