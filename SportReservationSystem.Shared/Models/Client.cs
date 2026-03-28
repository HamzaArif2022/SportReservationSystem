using System;
using System.Collections.Generic;

namespace SportReservationSystem.Shared.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Role { get; set; } = "Client"; // "Client" ou "Gestionnaire"
        public bool EstActif { get; set; } = true;
        public DateTime DateInscription { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}