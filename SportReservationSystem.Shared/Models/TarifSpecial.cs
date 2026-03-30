using System;

namespace SportReservationSystem.Shared.Models
{
    public class TarifSpecial
    {
        public int Id { get; set; }
        public int TerrainId { get; set; }
        public int JourSemaine { get; set; } // 0 = Dimanche, 1 = Lundi, ..., 6 = Samedi
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public decimal Tarif { get; set; }  // Changé de "TarifSpecial" à "Tarif"
        
        // Navigation property
        public Terrain Terrain { get; set; } = null!;
    }
}