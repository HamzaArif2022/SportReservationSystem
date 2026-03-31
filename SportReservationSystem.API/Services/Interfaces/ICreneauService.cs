using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface ICreneauService
    {
        Task<List<CreneauDto>> GetDisponiblesAsync(int terrainId, DateTime date);
        Task<CreneauDto?> GetByIdAsync(int id);
        Task<Creneau> CreateAsync(Creneau creneau);
        Task<List<CreneauDto>> GetAllAsync();
        Task<bool> UpdateDisponibiliteAsync(int id, bool estDisponible);
    }
}