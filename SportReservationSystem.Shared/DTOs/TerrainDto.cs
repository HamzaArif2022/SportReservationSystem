namespace SportReservationSystem.Shared.DTOs
{
    public class TerrainDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string TypeSport { get; set; } = string.Empty;
        public decimal TarifHoraire { get; set; }
        public int Capacite { get; set; }
        public string Statut { get; set; } = string.Empty;
    }

    public class CreateTerrainDto
    {
        public string Nom { get; set; } = string.Empty;
        public string TypeSport { get; set; } = string.Empty;
        public decimal TarifHoraire { get; set; }
        public int Capacite { get; set; }
    }

    public class UpdateTerrainDto
    {
        public string? Nom { get; set; }
        public string? TypeSport { get; set; }
        public decimal? TarifHoraire { get; set; }
        public int? Capacite { get; set; }
        public string? Statut { get; set; }
    }
}