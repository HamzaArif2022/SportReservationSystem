using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface IPaiementService
    {
        /// <summary>
        /// Crée un nouveau paiement pour une réservation
        /// </summary>
        Task<Paiement?> CreateAsync(CreatePaiementDto dto);
        
        /// <summary>
        /// Récupère un paiement par son ID
        /// </summary>
        Task<Paiement?> GetByIdAsync(int id);
        
        /// <summary>
        /// Récupère tous les paiements d'un client
        /// </summary>
        Task<List<PaiementDto>> GetByClientIdAsync(int clientId);
        
        /// <summary>
        /// Récupère le paiement associé à une réservation
        /// </summary>
        Task<Paiement?> GetByReservationIdAsync(int reservationId);
        
        /// <summary>
        /// Traite le remboursement d'un paiement
        /// </summary>
        Task<bool> RefundAsync(int paiementId);
        
        /// <summary>
        /// Génère une facture au format texte (simulation)
        /// </summary>
        Task<string> GenerateInvoiceAsync(int paiementId);
        
        /// <summary>
        /// Vérifie si une réservation est déjà payée
        /// </summary>
        Task<bool> IsReservationPayeeAsync(int reservationId);
    }
}