using System.Collections.Generic;

namespace SportReservationSystem.Shared.Models
{
    public class Terrain
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;  // ← Notez "Nom" pas "Name"
        public string TypeSport { get; set; } = string.Empty;
        public decimal TarifHoraire { get; set; }
        public int Capacite { get; set; }
        public string Statut { get; set; } = "Disponible";
        
        public ICollection<Creneau> Creneaux { get; set; } = new List<Creneau>();
        public ICollection<TarifSpecial> TarifsSpeciaux { get; set; } = new List<TarifSpecial>();
    }
}