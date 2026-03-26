using Microsoft.EntityFrameworkCore;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Terrain> Terrains => Set<Terrain>();
    public DbSet<Creneau> Creneaux => Set<Creneau>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Reservation>()
            .HasOne(x => x.Client)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(x => x.Creneau)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.CreneauId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Creneau>()
            .HasOne(x => x.Terrain)
            .WithMany(x => x.Creneaux)
            .HasForeignKey(x => x.TerrainId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
