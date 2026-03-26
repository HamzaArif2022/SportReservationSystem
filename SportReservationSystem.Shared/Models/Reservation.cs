namespace SportReservationSystem.Shared.Models;

public class Reservation
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int CreneauId { get; set; }
    public string Status { get; set; } = "En attente";

    public Client? Client { get; set; }
    public Creneau? Creneau { get; set; }
}
