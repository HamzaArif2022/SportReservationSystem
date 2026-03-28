using System;

namespace SportReservationSystem.Shared.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientNom { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime DateEnvoi { get; set; }
        public bool EstLu { get; set; }
    }

    public class CreateNotificationDto
    {
        public int ClientId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}