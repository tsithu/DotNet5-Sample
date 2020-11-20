using Microsoft.EntityFrameworkCore;
using DotNet5WebApi.Models;

namespace DotNet5WebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        public DbSet<Transaction> Transactions { get; set; }
    }
}