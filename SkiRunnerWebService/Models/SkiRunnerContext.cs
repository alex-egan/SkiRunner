using Microsoft.EntityFrameworkCore;
using SkiRunnerWebService.Models;
using Microsoft.EntityFrameworkCore.Proxies;
using Serilog;

public class SkiRunnerContext : DbContext
{
    public DbSet<Resort> Resorts { get; set; } = null!;
    public DbSet<ResortEntity> ResortEntities { get; set; } = null!;

    public string DbPath { get; } = null!;

    public SkiRunnerContext()
    {
        Log.Information("Creating SkiRunnerContext.");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseLazyLoadingProxies()
            .UseMySQL("server=localhost;database=SkiRunner;user=root;password=AlSnow13!!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Resort>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired();
      });

      modelBuilder.Entity<Resort>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired();
        entity.HasMany(r => r.ResortEntities)
              .WithOne(r => r.Resort);
      });

      modelBuilder.Entity<ResortEntity>(entity => 
      {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Name).IsRequired();
        entity.Property(r => r.Type).IsRequired();
        entity.HasMany(r => r.AccessibleEntities)
              .WithOne(r => r.ParentEntity);
      });
    }
}