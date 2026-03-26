namespace SportReservationSystem.Shared.DTOs;

public class ReservationDto
{
    public int ClientId { get; set; }
    public int CreneauId { get; set; }
    public string Status { get; set; } = "Confirmée";
}
