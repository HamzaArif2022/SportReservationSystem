using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface ICreneauService
    {
        Task<List<Creneau>> GetDisponiblesAsync(int terrainId, DateTime date);
        Task<Creneau?> GetByIdAsync(int id);
        Task<Creneau> CreateAsync(Creneau creneau);
        Task<List<Creneau>> GetAllAsync();
        Task<bool> UpdateDisponibiliteAsync(int id, bool estDisponible);
    }
}