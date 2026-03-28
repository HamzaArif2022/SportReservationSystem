using System;

namespace SportReservationSystem.Shared.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CreneauId { get; set; }
        public DateTime DateReservation { get; set; } = DateTime.Now;
        public string Statut { get; set; } = "En attente";  // ← Notez "Statut" pas "Status"
        public decimal MontantTotal { get; set; }
        public string? QrCode { get; set; }
        
        public Client Client { get; set; } = null!;
        public Creneau Creneau { get; set; } = null!;
        public Paiement? Paiement { get; set; }
    }
}