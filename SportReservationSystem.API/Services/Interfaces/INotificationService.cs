using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Envoie une notification à un client
        /// </summary>
        Task<Notification> SendNotificationAsync(CreateNotificationDto dto);
        
        /// <summary>
        /// Envoie un email de confirmation de réservation
        /// </summary>
        Task SendReservationConfirmationAsync(int reservationId);
        
        /// <summary>
        /// Envoie un email de validation par le gestionnaire
        /// </summary>
        Task SendReservationValidatedAsync(int reservationId);
        
        /// <summary>
        /// Envoie un email de refus de réservation
        /// </summary>
        Task SendReservationRefusedAsync(int reservationId, string motif);
        
        /// <summary>
        /// Envoie un email de confirmation d'annulation
        /// </summary>
        Task SendReservationCancelledAsync(int reservationId);
        
        /// <summary>
        /// Envoie un SMS de rappel (simulation)
        /// </summary>
        Task SendSmsReminderAsync(int reservationId);
        
        /// <summary>
        /// Récupère toutes les notifications d'un client
        /// </summary>
        Task<List<NotificationDto>> GetByClientIdAsync(int clientId);
        
        /// <summary>
        /// Marque une notification comme lue
        /// </summary>
        Task MarkAsReadAsync(int notificationId);
        
        /// <summary>
        /// Récupère les notifications non lues d'un client
        /// </summary>
        Task<List<NotificationDto>> GetUnreadByClientIdAsync(int clientId);
    }
}