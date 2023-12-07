using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class AppDataContext : DbContext
{
   public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

   public DbSet<Categoria> Categorias { get; set; }
   public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

         

    } 
}