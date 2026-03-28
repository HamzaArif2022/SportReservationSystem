using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface IStatistiqueService
    {
        /// <summary>
        /// Récupère les statistiques du tableau de bord
        /// </summary>
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        
        /// <summary>
        /// Récupère le taux d'occupation des terrains
        /// </summary>
        Task<TauxOccupationDto> GetTauxOccupationAsync(DateTime? debut = null, DateTime? fin = null);
        
        /// <summary>
        /// Récupère le chiffre d'affaires
        /// </summary>
        Task<ChiffreAffairesDto> GetChiffreAffairesAsync(DateTime? debut = null, DateTime? fin = null);
        
        /// <summary>
        /// Récupère le top des terrains les plus réservés
        /// </summary>
        Task<List<TerrainOccupationDto>> GetTopTerrainsAsync(int top = 5);
        
        /// <summary>
        /// Récupère les créneaux horaires les plus demandés
        /// </summary>
        Task<List<CreneauPopulariteDto>> GetCreneauxPopulairesAsync();
        
        /// <summary>
        /// Exporte les statistiques en texte (simulation)
        /// </summary>
        Task<byte[]> ExportRapportAsync(DateTime debut, DateTime fin);
    }
}