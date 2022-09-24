using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiPreAceleracionAlkemy.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
      
    }

}
