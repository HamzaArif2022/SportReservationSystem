using System;
using System.Collections.Generic;

namespace SportReservationSystem.Shared.Models
{
    public class Creneau
    {
        public int Id { get; set; }
        public int TerrainId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan HeureDebut { get; set; }  // ← Notez "HeureDebut" pas "StartTime"
        public TimeSpan HeureFin { get; set; }    // ← Notez "HeureFin" pas "EndTime"
        public bool EstDisponible { get; set; } = true;  // ← Notez "EstDisponible" pas "IsAvailable"
        
        public Terrain Terrain { get; set; } = null!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        
        public string DateHeure  => $"{Date:dd/MM/yyyy} - {HeureDebut:hh\\:mm} à {HeureFin:hh\\:mm}";
    }
}