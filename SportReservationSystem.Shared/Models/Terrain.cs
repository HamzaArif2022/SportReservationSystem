namespace SportReservationSystem.Shared.Models;

public class Terrain
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public ICollection<Creneau> Creneaux { get; set; } = new List<Creneau>();
}
