using System;

namespace SportReservationSystem.Shared.DTOs
{
    public class CreneauDto
    {
        public int Id { get; set; }
        public int TerrainId { get; set; }
        public string TerrainNom { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public bool EstDisponible { get; set; }

        // infos terrain
        public int Capacite { get; set; }
        public decimal TarifHoraire { get; set; }
        public string TypeSport { get; set; } = string.Empty;

        
        public string DateHeure => $"{Date:dd/MM/yyyy} - {HeureDebut:hh\\:mm} à {HeureFin:hh\\:mm}";
    }

    public class CreneauxDisponiblesRequestDto
    {
        public int TerrainId { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreateCreneauDto
    {
        public int TerrainId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
    }
}