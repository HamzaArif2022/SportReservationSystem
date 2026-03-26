using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces;

public interface ITerrainService
{
    Task<List<Terrain>> GetAllAsync();
}
