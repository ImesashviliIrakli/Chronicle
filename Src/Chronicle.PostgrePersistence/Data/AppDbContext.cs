using Chronicle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chronicle.PostgrePersistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Todo> Todos { get; set; }
}
