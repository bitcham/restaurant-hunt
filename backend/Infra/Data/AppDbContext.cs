using Core.Domain.Entities;
using Infra.Data.EntityMapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserMapping());
    }
}