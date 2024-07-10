using Microsoft.EntityFrameworkCore;
using SkiRunnerWebService.Models;
using Microsoft.EntityFrameworkCore.Proxies;

public class SkiRunnerContext : DbContext
{
    public DbSet<Resort> Resorts { get; set; }
    public DbSet<Lift> Lifts { get; set; }
    public DbSet<Run> Runs { get; set; }

    public string DbPath { get; }

    public SkiRunnerContext()
    {
        Console.WriteLine("Creating SkiRunner Context");
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = Path.Join(path, "skirunner.db");
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

      modelBuilder.Entity<Lift>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).IsRequired();
        entity.HasOne(r => r.Resort)
          .WithMany(r => r.Lifts)
          .HasForeignKey(l => l.ResortId)
          .IsRequired();
      });

      modelBuilder.Entity<Run>(entity => 
      {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Name).IsRequired();
        entity.HasMany(r => r.AccessibleRuns)
            .WithOne()
            .HasForeignKey(r => r.ParentRunId)
            .IsRequired(false);
        entity.HasOne(r => r.Lift)
            .WithMany(l => l.AccessibleRuns)
            .HasForeignKey(r => r.LiftId)
            .IsRequired(false);
      });
    }
}