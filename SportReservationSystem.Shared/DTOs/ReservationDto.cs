namespace SportReservationSystem.Shared.DTOs
{
    public class CreateReservationDto
    {
        public int CreneauId { get; set; }
        public decimal MontantTotal { get; set; }
    }

    public class ReservationDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientNom { get; set; } = string.Empty;
        public string ClientPrenom { get; set; } = string.Empty;
        public int CreneauId { get; set; }
        public string TerrainNom { get; set; } = string.Empty;
        public string TerrainTypeSport { get; set; } = string.Empty;
        public DateTime DateCreneau { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public DateTime DateReservation { get; set; }
        public string Statut { get; set; } = string.Empty;  // ← "Statut" pas "Status"
        public decimal MontantTotal { get; set; }
        public string? QrCode { get; set; }
        public bool EstPaye { get; set; }
    }
}