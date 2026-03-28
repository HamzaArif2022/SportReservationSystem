using System;

namespace SportReservationSystem.Shared.DTOs
{
    public class CreatePaiementDto
    {
        public int ReservationId { get; set; }
        public decimal Montant { get; set; }
        public string ModePaiement { get; set; } = "Carte";
    }

    public class PaiementDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Montant { get; set; }
        public DateTime DatePaiement { get; set; }
        public string ModePaiement { get; set; } = string.Empty;
        public string Statut { get; set; } = string.Empty;
        public string? ReferenceFacture { get; set; }
    }
}