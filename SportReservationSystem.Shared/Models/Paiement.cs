using System;

namespace SportReservationSystem.Shared.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Montant { get; set; }
        public DateTime DatePaiement { get; set; } = DateTime.Now;
        public string ModePaiement { get; set; } = "Carte";
        public string Statut { get; set; } = "Effectué";
        public string? ReferenceFacture { get; set; }
    
        public Reservation Reservation { get; set; } = null!;
    }
}