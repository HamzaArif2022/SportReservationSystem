using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync();
        Task<List<ReservationDto>> GetByClientIdAsync(int clientId);
        Task<List<ReservationDto>> GetEnAttenteAsync();
        Task<ReservationDto?> CreateAsync(CreateReservationDto dto, int clientId);
        Task<bool> CancelAsync(int reservationId, int clientId);
        Task<ReservationDto?> ValidateAsync(int reservationId);
        Task<ReservationDto?> RefuseAsync(int reservationId, string motif);
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<List<ReservationDto>> GetPlanningAsync(DateTime date);
    }
}