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

    public async Task<List<ReservationDto>> GetAllAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .OrderByDescending(r => r.Id)
            .ToListAsync();
        
        return reservations.Select(r => MapToDto(r)).ToList();
    }

    public async Task<List<ReservationDto>> GetByClientIdAsync(int clientId)
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.Id)
            .ToListAsync();
        
        return reservations.Select(r => MapToDto(r)).ToList();
    }

    public async Task<List<ReservationDto>> GetEnAttenteAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .Where(r => r.Statut == "En attente")
            .OrderBy(r => r.Creneau!.Date)
            .ThenBy(r => r.Creneau!.HeureDebut)
            .ToListAsync();
        
        return reservations.Select(r => MapToDto(r)).ToList();
    }

    public async Task<ReservationDto?> CreateAsync(CreateReservationDto dto, int clientId)
    {
        var creneau = await _dbContext.Creneaux.FirstOrDefaultAsync(c => c.Id == dto.CreneauId);
        if (creneau is null || !creneau.EstDisponible)
        {
            return null;
        }

        var reservation = new Reservation
        {
            ClientId = clientId,
            CreneauId = dto.CreneauId,
            Statut = "En attente",
            DateReservation = DateTime.Now,
            MontantTotal = dto.MontantTotal,
            QrCode = GenerateQrCode()
        };

        creneau.EstDisponible = false;
        _dbContext.Reservations.Add(reservation);
        await _dbContext.SaveChangesAsync();

        return MapToDto(reservation);
    }

    public async Task<bool> CancelAsync(int reservationId, int clientId)
    {
        var reservation = await _dbContext.Reservations
            .Include(r => r.Creneau)
            .FirstOrDefaultAsync(r => r.Id == reservationId && r.ClientId == clientId);
        
        if (reservation is null)
            return false;
        
        if (reservation.Creneau!.Date < DateTime.Now.AddDays(1))
            return false;
        
        if (reservation.Statut != "Confirmée" && reservation.Statut != "En attente")
            return false;

        reservation.Statut = "Annulée";
        var creneau = await _dbContext.Creneaux.FirstOrDefaultAsync(c => c.Id == reservation.CreneauId);
        if (creneau is not null)
        {
            creneau.EstDisponible = true;
        }

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ReservationDto?> ValidateAsync(int reservationId)
    {
        var reservation = await _dbContext.Reservations
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .FirstOrDefaultAsync(r => r.Id == reservationId);
        
        if (reservation is null || reservation.Statut != "En attente")
            return null;

        reservation.Statut = "Confirmée";
        await _dbContext.SaveChangesAsync();

        return MapToDto(reservation);
    }

    public async Task<ReservationDto?> RefuseAsync(int reservationId, string motif)
    {
        var reservation = await _dbContext.Reservations
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .FirstOrDefaultAsync(r => r.Id == reservationId);
        
        if (reservation is null || reservation.Statut != "En attente")
            return null;

        reservation.Statut = "Refusée";
        
        var creneau = await _dbContext.Creneaux.FindAsync(reservation.CreneauId);
        if (creneau is not null)
        {
            creneau.EstDisponible = true;
        }

        await _dbContext.SaveChangesAsync();

        return MapToDto(reservation);
    }

    public async Task<ReservationDto?> GetByIdAsync(int id)
    {
        var reservation = await _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .FirstOrDefaultAsync(r => r.Id == id);
        
        return reservation is null ? null : MapToDto(reservation);
    }

    public async Task<List<ReservationDto>> GetPlanningAsync(DateTime date)
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
            .Where(r => r.Creneau!.Date == date.Date && (r.Statut == "Confirmée" || r.Statut == "En attente"))
            .OrderBy(r => r.Creneau!.HeureDebut)
            .ToListAsync();
        
        return reservations.Select(r => MapToDto(r)).ToList();
    }

    // Méthode helper pour mapper Reservation → ReservationDto
    private ReservationDto MapToDto(Reservation reservation)
    {
        return new ReservationDto
        {
            Id = reservation.Id,
            ClientId = reservation.ClientId,
            ClientNom = reservation.Client?.Nom ?? string.Empty,
            ClientPrenom = reservation.Client?.Prenom ?? string.Empty,
            CreneauId = reservation.CreneauId,
            TerrainNom = reservation.Creneau?.Terrain?.Nom ?? string.Empty,
            TerrainTypeSport = reservation.Creneau?.Terrain?.TypeSport ?? string.Empty,
            DateCreneau = reservation.Creneau?.Date ?? DateTime.MinValue,
            HeureDebut = reservation.Creneau?.HeureDebut ?? TimeSpan.Zero,
            HeureFin = reservation.Creneau?.HeureFin ?? TimeSpan.Zero,
            DateReservation = reservation.DateReservation,
            Statut = reservation.Statut,
            MontantTotal = reservation.MontantTotal,
            QrCode = reservation.QrCode,
            EstPaye = false // À gérer avec PaiementService
        };
    }

    private string GenerateQrCode()
    {
        return $"QR-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
    }
}