using System;

namespace SportReservationSystem.Shared.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime DateEnvoi { get; set; } = DateTime.Now;
        public bool EstLu { get; set; } = false;
    
        public Client Client { get; set; } = null!;
    }
}