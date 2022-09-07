using ECA.Backend.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ECA.Backend.Data.Context
{
    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<UserData>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
