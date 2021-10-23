using APPConsola1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPConsola1.Context
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Posts> Posts { get; set; }
    }
}
