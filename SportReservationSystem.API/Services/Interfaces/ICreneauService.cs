using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces;

public interface ICreneauService
{
    Task<List<Creneau>> GetByTerrainAsync(int terrainId);
    Task<List<Creneau>> SearchAvailableAsync(DateTime? date);
}
