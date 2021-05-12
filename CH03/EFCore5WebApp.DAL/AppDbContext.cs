using EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore5WebApp.DAL {
    public class AppDbContext : DbContext {
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions options) : base(options) {}
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LookUp> LookUps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}