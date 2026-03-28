using System;
using System.Collections.Generic;

namespace SportReservationSystem.Shared.DTOs
{
    public class TauxOccupationDto
    {
        public string Periode { get; set; } = string.Empty;
        public decimal TauxOccupationGlobal { get; set; }
        public List<TerrainOccupationDto> TerrainsOccupation { get; set; } = new();
        public List<CreneauPopulariteDto> CreneauxPopulaires { get; set; } = new();
    }

    public class TerrainOccupationDto
    {
        public int TerrainId { get; set; }
        public string TerrainNom { get; set; } = string.Empty;
        public decimal TauxOccupation { get; set; }
        public int NombreReservations { get; set; }
        public decimal ChiffreAffaires { get; set; }
    }

    public class CreneauPopulariteDto
    {
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public int NombreReservations { get; set; }
        public decimal TauxOccupation { get; set; }
    }

    public class ChiffreAffairesDto
    {
        public decimal Total { get; set; }
        public decimal Evolution { get; set; } // Pourcentage d'évolution
        public Dictionary<string, decimal> ParMois { get; set; } = new();
        public Dictionary<string, decimal> ParTerrain { get; set; } = new();
    }

    public class DashboardStatsDto
    {
        public int NombreReservationsAujourdHui { get; set; }
        public int NombreReservationsEnAttente { get; set; }
        public int NombreReservationsConfirmées { get; set; }
        public decimal ChiffreAffairesMois { get; set; }
        public decimal TauxOccupationMois { get; set; }
        public List<ReservationDto> DernieresReservations { get; set; } = new();
        public List<TerrainOccupationDto> TopTerrains { get; set; } = new();
    }
}