using System;
using CoreNRF.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreNRF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<NF> NFs { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new NFConfiguration());
            builder.ApplyConfiguration(new ServicesConfiguration());
        }
        public DatabaseFacade GetDatabase()
        {
            return Database;
            
        }

    }
    public class ApplicationDbContextFctory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql("server = localhost; port = 3306; database = coreNRF; user = root; password = Cardinals25!", new MySqlServerVersion(new Version("8.0.28")));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
