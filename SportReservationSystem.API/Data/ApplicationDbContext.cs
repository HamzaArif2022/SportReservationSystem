using Microsoft.EntityFrameworkCore;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Creneau> Creneaux { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TarifSpecial> TarifsSpeciaux { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================
            // CONFIGURATION DE LA TABLE CLIENT
            // ============================================
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.Telephone)
                    .HasMaxLength(20);
                
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Client");
                
                entity.Property(e => e.EstActif)
                    .HasDefaultValue(true);
                
                entity.Property(e => e.DateInscription)
                    .HasDefaultValueSql("GETDATE()");
            });

            // ============================================
            // CONFIGURATION DE LA TABLE TERRAIN
            // ============================================
            modelBuilder.Entity<Terrain>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.TypeSport)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.TarifHoraire)
                    .IsRequired()
                    .HasPrecision(10, 2);
                
                entity.Property(e => e.Capacite)
                    .HasDefaultValue(0);
                
                entity.Property(e => e.Statut)
                    .HasMaxLength(20)
                    .HasDefaultValue("Disponible");
            });

            // ============================================
            // CONFIGURATION DE LA TABLE CRENEAU
            // ============================================
            modelBuilder.Entity<Creneau>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Date)
                    .IsRequired();
                
                entity.Property(e => e.HeureDebut)
                    .IsRequired();
                
                entity.Property(e => e.HeureFin)
                    .IsRequired();
                
                entity.Property(e => e.EstDisponible)
                    .HasDefaultValue(true);
                
                // Vérification que HeureDebut < HeureFin
                entity.HasCheckConstraint("CK_Creneau_Heures", "[HeureDebut] < [HeureFin]");
                
                // Relation avec Terrain
                entity.HasOne(e => e.Terrain)
                    .WithMany(t => t.Creneaux)
                    .HasForeignKey(e => e.TerrainId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================================
            // CONFIGURATION DE LA TABLE RESERVATION
            // ============================================
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.DateReservation)
                    .HasDefaultValueSql("GETDATE()");
                
                entity.Property(e => e.Statut)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("En attente");
                
                entity.Property(e => e.MontantTotal)
                    .IsRequired()
                    .HasPrecision(10, 2);
                
                entity.Property(e => e.QrCode)
                    .HasMaxLength(500);
                
                // Vérification des statuts valides
                entity.HasCheckConstraint("CK_Reservation_Statut", 
                    "[Statut] IN ('En attente', 'Confirmée', 'Refusée', 'Annulée', 'Terminée')");
                
                // Relation avec Client
                entity.HasOne(e => e.Client)
                    .WithMany(c => c.Reservations)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // Relation avec Creneau
                entity.HasOne(e => e.Creneau)
                    .WithMany(c => c.Reservations)
                    .HasForeignKey(e => e.CreneauId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // CONFIGURATION DE LA TABLE PAIEMENT
            // ============================================
            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Montant)
                    .IsRequired()
                    .HasPrecision(10, 2);
                
                entity.Property(e => e.DatePaiement)
                    .HasDefaultValueSql("GETDATE()");
                
                entity.Property(e => e.ModePaiement)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Carte");
                
                entity.Property(e => e.Statut)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Effectué");
                
                entity.Property(e => e.ReferenceFacture)
                    .HasMaxLength(100);
                
                // Vérification des statuts valides
                entity.HasCheckConstraint("CK_Paiement_Statut", 
                    "[Statut] IN ('Effectué', 'Remboursé')");
                
                // Relation avec Reservation (1-1)
                entity.HasOne(e => e.Reservation)
                    .WithOne(r => r.Paiement)
                    .HasForeignKey<Paiement>(e => e.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Index unique sur ReservationId
                entity.HasIndex(e => e.ReservationId)
                    .IsUnique();
            });

            // ============================================
            // CONFIGURATION DE LA TABLE NOTIFICATION
            // ============================================
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(e => e.DateEnvoi)
                    .HasDefaultValueSql("GETDATE()");
                
                entity.Property(e => e.EstLu)
                    .HasDefaultValue(false);
                
                // Vérification des types valides
                entity.HasCheckConstraint("CK_Notification_Type", 
                    "[Type] IN ('Email', 'SMS')");
                
                // Relation avec Client
                entity.HasOne(e => e.Client)
                    .WithMany(c => c.Notifications)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Index pour optimiser les recherches par client
                entity.HasIndex(e => e.ClientId);
                entity.HasIndex(e => e.EstLu);
            });

            // ============================================
            // CONFIGURATION DE LA TABLE TARIF_SPECIAL
            // ============================================
            modelBuilder.Entity<TarifSpecial>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.JourSemaine)
                    .IsRequired();
                
                entity.Property(e => e.HeureDebut)
                    .IsRequired();
                
                entity.Property(e => e.HeureFin)
                    .IsRequired();
                
                entity.Property(e => e.Tarif)
                    .HasColumnName("Tarif");
                
                // Vérification du jour (0-6)
                entity.HasCheckConstraint("CK_TarifSpecial_Jour", 
                    "[JourSemaine] BETWEEN 0 AND 6");
                
                // Vérification des heures
                entity.HasCheckConstraint("CK_TarifSpecial_Heures", 
                    "[HeureDebut] < [HeureFin]");
                
                // Relation avec Terrain
                entity.HasOne(e => e.Terrain)
                    .WithMany(t => t.TarifsSpeciaux)
                    .HasForeignKey(e => e.TerrainId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Index pour optimiser les recherches par terrain et jour
                entity.HasIndex(e => new { e.TerrainId, e.JourSemaine });
            });
        }
    }
}