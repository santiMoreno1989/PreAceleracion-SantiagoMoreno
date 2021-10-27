using APPConsola1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPConsola1.Context
{
   public class ApplicationDbContext : DbContext
    {
        private const string Schema = "Publicaciones";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=PosteoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

            //Informacion precargada en la entidad Usuario //
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Name = "Santiago",
                    Password = "Nueva1234",
                    Email = "correo@ejemplo.com"
                });
        }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
    }
}
