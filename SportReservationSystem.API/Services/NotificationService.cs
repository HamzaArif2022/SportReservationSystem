using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> SendNotificationAsync(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                ClientId = dto.ClientId,
                Type = dto.Type,
                Message = dto.Message,
                DateEnvoi = DateTime.Now,
                EstLu = false
            };
            
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            
            return notification;
        }

        public async Task SendReservationConfirmationAsync(int reservationId)
        {
            var reservation = await GetReservationWithDetailsAsync(reservationId);
            if (reservation == null) return;
            
            var message = $@"
Bonjour {reservation.Client.Prenom} {reservation.Client.Nom},

Votre réservation pour le terrain '{reservation.Creneau.Terrain.Nom}' 
le {reservation.Creneau.Date:dd/MM/yyyy} de {reservation.Creneau.HeureDebut:hh\\:mm} à {reservation.Creneau.HeureFin:hh\\:mm}
a bien été confirmée.

Montant total : {reservation.MontantTotal} DH
QR Code : {reservation.QrCode}

Merci de votre confiance !
Sport'Arena";

            await SendNotificationAsync(new CreateNotificationDto
            {
                ClientId = reservation.ClientId,
                Type = "Email",
                Message = message
            });
        }

        public async Task SendReservationValidatedAsync(int reservationId)
        {
            var reservation = await GetReservationWithDetailsAsync(reservationId);
            if (reservation == null) return;
            
            var message = $@"
Bonjour {reservation.Client.Prenom} {reservation.Client.Nom},

Votre réservation pour le terrain '{reservation.Creneau.Terrain.Nom}' 
le {reservation.Creneau.Date:dd/MM/yyyy} de {reservation.Creneau.HeureDebut:hh\\:mm} à {reservation.Creneau.HeureFin:hh\\:mm}
a été VALIDÉE par le gestionnaire.

Vous pouvez accéder au terrain avec le QR code ci-joint.

Sport'Arena";

            await SendNotificationAsync(new CreateNotificationDto
            {
                ClientId = reservation.ClientId,
                Type = "Email",
                Message = message
            });
        }

        public async Task SendReservationRefusedAsync(int reservationId, string motif)
        {
            var reservation = await GetReservationWithDetailsAsync(reservationId);
            if (reservation == null) return;
            
            var message = $@"
Bonjour {reservation.Client.Prenom} {reservation.Client.Nom},

Nous sommes désolés de vous informer que votre réservation pour le terrain '{reservation.Creneau.Terrain.Nom}' 
le {reservation.Creneau.Date:dd/MM/yyyy} de {reservation.Creneau.HeureDebut:hh\\:mm} à {reservation.Creneau.HeureFin:hh\\:mm}
a été REFUSÉE.

Motif : {motif}

Un remboursement sera effectué sous 48h.

Sport'Arena";

            await SendNotificationAsync(new CreateNotificationDto
            {
                ClientId = reservation.ClientId,
                Type = "Email",
                Message = message
            });
        }

        public async Task SendReservationCancelledAsync(int reservationId)
        {
            var reservation = await GetReservationWithDetailsAsync(reservationId);
            if (reservation == null) return;
            
            var message = $@"
Bonjour {reservation.Client.Prenom} {reservation.Client.Nom},

Votre réservation pour le terrain '{reservation.Creneau.Terrain.Nom}' 
le {reservation.Creneau.Date:dd/MM/yyyy} de {reservation.Creneau.HeureDebut:hh\\:mm} à {reservation.Creneau.HeureFin:hh\\:mm}
a été ANNULÉE.

Le remboursement de {reservation.MontantTotal} DH sera effectué sous 48h.

Sport'Arena";

            await SendNotificationAsync(new CreateNotificationDto
            {
                ClientId = reservation.ClientId,
                Type = "Email",
                Message = message
            });
        }

        public async Task SendSmsReminderAsync(int reservationId)
        {
            var reservation = await GetReservationWithDetailsAsync(reservationId);
            if (reservation == null) return;
            
            var message = $"Rappel : Votre réservation pour {reservation.Creneau.Terrain.Nom} le {reservation.Creneau.Date:dd/MM/yyyy} à {reservation.Creneau.HeureDebut:hh\\:mm}. Sport'Arena";
            
            await SendNotificationAsync(new CreateNotificationDto
            {
                ClientId = reservation.ClientId,
                Type = "SMS",
                Message = message
            });
        }

        public async Task<List<NotificationDto>> GetByClientIdAsync(int clientId)
        {
            return await _context.Notifications
                .Where(n => n.ClientId == clientId)
                .OrderByDescending(n => n.DateEnvoi)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    ClientId = n.ClientId,
                    ClientNom = n.Client.Nom,
                    Type = n.Type,
                    Message = n.Message,
                    DateEnvoi = n.DateEnvoi,
                    EstLu = n.EstLu
                })
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.EstLu = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NotificationDto>> GetUnreadByClientIdAsync(int clientId)
        {
            return await _context.Notifications
                .Where(n => n.ClientId == clientId && !n.EstLu)
                .OrderByDescending(n => n.DateEnvoi)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    ClientId = n.ClientId,
                    ClientNom = n.Client.Nom,
                    Type = n.Type,
                    Message = n.Message,
                    DateEnvoi = n.DateEnvoi,
                    EstLu = n.EstLu
                })
                .ToListAsync();
        }

        private async Task<Reservation?> GetReservationWithDetailsAsync(int reservationId)
        {
            return await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Creneau)
                .ThenInclude(c => c!.Terrain)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }
    }
}