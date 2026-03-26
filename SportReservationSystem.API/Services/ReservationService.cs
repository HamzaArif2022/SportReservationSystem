using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Reservation>> GetAllAsync()
    {
        return _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
            .ThenInclude(c => c!.Terrain)
            .OrderByDescending(r => r.Id)
            .ToListAsync();
    }

    public Task<List<Reservation>> GetByClientAsync(int clientId)
    {
        return _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Creneau)
            .ThenInclude(c => c!.Terrain)
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.Id)
            .ToListAsync();
    }

    public async Task<Reservation?> CreateAsync(ReservationDto dto)
    {
        var creneau = await _dbContext.Creneaux.FirstOrDefaultAsync(c => c.Id == dto.CreneauId);
        if (creneau is null || !creneau.IsAvailable)
        {
            return null;
        }

        var reservation = new Reservation
        {
            ClientId = dto.ClientId,
            CreneauId = dto.CreneauId,
            Status = string.IsNullOrWhiteSpace(dto.Status) ? "Confirmée" : dto.Status
        };

        creneau.IsAvailable = false;
        _dbContext.Reservations.Add(reservation);
        await _dbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task<bool> CancelAsync(int reservationId)
    {
        var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
        if (reservation is null)
        {
            return false;
        }

        reservation.Status = "Annulée";
        var creneau = await _dbContext.Creneaux.FirstOrDefaultAsync(c => c.Id == reservation.CreneauId);
        if (creneau is not null)
        {
            creneau.IsAvailable = true;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateAsync(int reservationId)
    {
        var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
        if (reservation is null)
        {
            return false;
        }

        reservation.Status = "Confirmée";
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
