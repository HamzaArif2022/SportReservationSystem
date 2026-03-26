using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces;

public interface IReservationService
{
    Task<List<Reservation>> GetAllAsync();
    Task<List<Reservation>> GetByClientAsync(int clientId);
    Task<Reservation?> CreateAsync(ReservationDto dto);
    Task<bool> CancelAsync(int reservationId);
    Task<bool> ValidateAsync(int reservationId);
}
