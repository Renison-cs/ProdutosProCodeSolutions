using Microsoft.EntityFrameworkCore;
using Produtos.Models;
namespace Produtos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
