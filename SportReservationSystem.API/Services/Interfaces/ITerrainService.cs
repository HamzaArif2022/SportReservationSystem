using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface ITerrainService
    {
        Task<List<Terrain>> GetAllAsync();
        Task<Terrain?> GetByIdAsync(int id);
        Task<Terrain> CreateAsync(Terrain terrain);
        Task<Terrain?> UpdateAsync(int id, Terrain terrain);
        Task<bool> DeleteAsync(int id);
        Task<List<Terrain>> GetDisponiblesAsync();
    }
}