namespace SportReservationSystem.Shared.Models;

public class Creneau
{
    public int Id { get; set; }
    public int TerrainId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Terrain? Terrain { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
