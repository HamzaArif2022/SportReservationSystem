using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services
{
    public class PaiementService : IPaiementService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public PaiementService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Paiement?> CreateAsync(CreatePaiementDto dto)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.Id == dto.ReservationId);
            
            if (reservation == null || reservation.MontantTotal != dto.Montant)
                return null;
            
            var existingPaiement = await _context.Paiements
                .FirstOrDefaultAsync(p => p.ReservationId == dto.ReservationId);
            
            if (existingPaiement != null)
                return null;
            
            var paiement = new Paiement
            {
                ReservationId = dto.ReservationId,
                Montant = dto.Montant,
                DatePaiement = DateTime.Now,
                ModePaiement = dto.ModePaiement,
                Statut = "Effectué",
                ReferenceFacture = GenerateReferenceFacture()
            };
            
            _context.Paiements.Add(paiement);
            reservation.Statut = "Confirmée";
            
            await _context.SaveChangesAsync();
            
            //await _notificationService.SendReservationConfirmationAsync(reservation.Id);
            
            return paiement;
        }

        public async Task<Paiement?> GetByIdAsync(int id)
        {
            return await _context.Paiements
                .Include(p => p.Reservation)
                .ThenInclude(r => r.Client)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PaiementDto>> GetByClientIdAsync(int clientId)
        {
            return await _context.Paiements
                .Include(p => p.Reservation)
                .Where(p => p.Reservation.ClientId == clientId)
                .OrderByDescending(p => p.DatePaiement)
                .Select(p => new PaiementDto
                {
                    Id = p.Id,
                    ReservationId = p.ReservationId,
                    Montant = p.Montant,
                    DatePaiement = p.DatePaiement,
                    ModePaiement = p.ModePaiement,
                    Statut = p.Statut,
                    ReferenceFacture = p.ReferenceFacture
                })
                .ToListAsync();
        }

        public async Task<Paiement?> GetByReservationIdAsync(int reservationId)
        {
            return await _context.Paiements
                .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }

        public async Task<bool> RefundAsync(int paiementId)
        {
            var paiement = await _context.Paiements
                .Include(p => p.Reservation)
                .ThenInclude(r => r.Creneau)
                .FirstOrDefaultAsync(p => p.Id == paiementId);
            
            if (paiement == null || paiement.Statut != "Effectué")
                return false;
            
            var creneau = paiement.Reservation.Creneau;
            if (creneau != null && creneau.Date < DateTime.Now.AddDays(1))
                return false;
            
            paiement.Statut = "Remboursé";
            paiement.Reservation.Statut = "Annulée";
            
            await _context.SaveChangesAsync();
            
            await _notificationService.SendReservationCancelledAsync(paiement.ReservationId);
            
            return true;
        }

        public async Task<string> GenerateInvoiceAsync(int paiementId)
        {
            var paiement = await GetByIdAsync(paiementId);
            if (paiement == null)
                return string.Empty;
            
            return $@"
====================================
FACTURE N° {paiement.ReferenceFacture}
====================================
Date : {paiement.DatePaiement:dd/MM/yyyy HH:mm}
Client : {paiement.Reservation.Client.Prenom} {paiement.Reservation.Client.Nom}
Email : {paiement.Reservation.Client.Email}

Réservation N° {paiement.ReservationId}
Terrain : {paiement.Reservation.Creneau?.Terrain?.Nom ?? "Non spécifié"}
Date : {paiement.Reservation.Creneau?.Date:dd/MM/yyyy}
Horaire : {paiement.Reservation.Creneau?.HeureDebut:hh\\:mm} - {paiement.Reservation.Creneau?.HeureFin:hh\\:mm}
Montant : {paiement.Montant} DH
Mode de paiement : {paiement.ModePaiement}

====================================
Merci de votre confiance !
Sport'Arena
====================================";
        }

        public async Task<bool> IsReservationPayeeAsync(int reservationId)
        {
            return await _context.Paiements
                .AnyAsync(p => p.ReservationId == reservationId && p.Statut == "Effectué");
        }

        private string GenerateReferenceFacture()
        {
            return $"FAC-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }
    }
}