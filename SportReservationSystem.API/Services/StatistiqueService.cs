using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Services
{
    public class StatistiqueService : IStatistiqueService
    {
        private readonly ApplicationDbContext _context;

        public StatistiqueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var aujourdHui = DateTime.Today;
            var debutMois = new DateTime(aujourdHui.Year, aujourdHui.Month, 1);
            
            var reservationsAujourdHui = await _context.Reservations
                .CountAsync(r => r.DateReservation.Date == aujourdHui);
            
            var reservationsEnAttente = await _context.Reservations
                .CountAsync(r => r.Statut == "En attente");
            
            var reservationsConfirmees = await _context.Reservations
                .CountAsync(r => r.Statut == "Confirmée");
            
            var chiffreAffairesMois = await _context.Paiements
                .Where(p => p.Statut == "Effectué" && p.DatePaiement >= debutMois)
                .SumAsync(p => p.Montant);
            
            var dernieresReservations = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Creneau)
                .ThenInclude(c => c.Terrain)
                .OrderByDescending(r => r.DateReservation)
                .Take(5)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    ClientNom = r.Client.Nom,
                    ClientPrenom = r.Client.Prenom,
                    TerrainNom = r.Creneau.Terrain.Nom,
                    DateCreneau = r.Creneau.Date,
                    HeureDebut = r.Creneau.HeureDebut,
                    HeureFin = r.Creneau.HeureFin,
                    Statut = r.Statut,
                    MontantTotal = r.MontantTotal
                })
                .ToListAsync();
            
            var topTerrains = await GetTopTerrainsAsync(5);
            
            var totalCreneauxMois = await _context.Creneaux
                .CountAsync(c => c.Date >= debutMois && c.Date <= aujourdHui);
            
            var reservationsMois = await _context.Reservations
                .CountAsync(r => r.DateReservation >= debutMois && r.Statut == "Confirmée");
            
            var tauxOccupationMois = totalCreneauxMois > 0 
                ? (decimal)reservationsMois / totalCreneauxMois * 100 
                : 0;
            
            return new DashboardStatsDto
            {
                NombreReservationsAujourdHui = reservationsAujourdHui,
                NombreReservationsEnAttente = reservationsEnAttente,
                NombreReservationsConfirmées = reservationsConfirmees,
                ChiffreAffairesMois = chiffreAffairesMois,
                TauxOccupationMois = Math.Round(tauxOccupationMois, 2),
                DernieresReservations = dernieresReservations,
                TopTerrains = topTerrains
            };
        }

        public async Task<TauxOccupationDto> GetTauxOccupationAsync(DateTime? debut = null, DateTime? fin = null)
        {
            debut ??= DateTime.Today.AddMonths(-1);
            fin ??= DateTime.Today;
            
            var terrainsOccupation = await _context.Terrains
                .Select(t => new TerrainOccupationDto
                {
                    TerrainId = t.Id,
                    TerrainNom = t.Nom,
                    NombreReservations = t.Creneaux
                        .SelectMany(c => c.Reservations)
                        .Count(r => r.DateReservation >= debut && r.DateReservation <= fin && r.Statut == "Confirmée"),
                    ChiffreAffaires = t.Creneaux
                        .SelectMany(c => c.Reservations)
                        .Where(r => r.DateReservation >= debut && r.DateReservation <= fin && r.Statut == "Confirmée")
                        .Sum(r => r.MontantTotal)
                })
                .ToListAsync();
            
            foreach (var terrain in terrainsOccupation)
            {
                var totalCreneaux = await _context.Creneaux
                    .CountAsync(c => c.TerrainId == terrain.TerrainId && c.Date >= debut && c.Date <= fin);
                
                terrain.TauxOccupation = totalCreneaux > 0 
                    ? (decimal)terrain.NombreReservations / totalCreneaux * 100 
                    : 0;
                terrain.TauxOccupation = Math.Round(terrain.TauxOccupation, 2);
            }
            
            var creneauxPopulaires = await GetCreneauxPopulairesAsync();
            
            return new TauxOccupationDto
            {
                Periode = $"{debut:dd/MM/yyyy} - {fin:dd/MM/yyyy}",
                TauxOccupationGlobal = Math.Round(terrainsOccupation.Average(t => t.TauxOccupation), 2),
                TerrainsOccupation = terrainsOccupation,
                CreneauxPopulaires = creneauxPopulaires
            };
        }

        public async Task<ChiffreAffairesDto> GetChiffreAffairesAsync(DateTime? debut = null, DateTime? fin = null)
        {
            debut ??= DateTime.Today.AddMonths(-1);
            fin ??= DateTime.Today;
            
            var paiements = await _context.Paiements
                .Where(p => p.Statut == "Effectué" && p.DatePaiement >= debut && p.DatePaiement <= fin)
                .ToListAsync();
            
            var total = paiements.Sum(p => p.Montant);
            
            var duree = fin.Value - debut.Value;
            var periodePrecedenteDebut = debut.Value.Subtract(duree);
            var periodePrecedenteFin = debut.Value.AddDays(-1);
            
            var totalPrecedent = await _context.Paiements
                .Where(p => p.Statut == "Effectué" && p.DatePaiement >= periodePrecedenteDebut && p.DatePaiement <= periodePrecedenteFin)
                .SumAsync(p => p.Montant);
            
            var evolution = totalPrecedent > 0 
                ? (total - totalPrecedent) / totalPrecedent * 100 
                : 0;
            
            var caParMois = new Dictionary<string, decimal>();
            for (var date = debut.Value; date <= fin.Value; date = date.AddMonths(1))
            {
                var moisDebut = new DateTime(date.Year, date.Month, 1);
                var moisFin = moisDebut.AddMonths(1).AddDays(-1);
                
                var caMois = await _context.Paiements
                    .Where(p => p.Statut == "Effectué" && p.DatePaiement >= moisDebut && p.DatePaiement <= moisFin)
                    .SumAsync(p => p.Montant);
                
                caParMois[$"{moisDebut:MM/yyyy}"] = caMois;
            }
            
            var caParTerrain = await _context.Terrains
                .Select(t => new
                {
                    t.Nom,
                    Ca = t.Creneaux
                        .SelectMany(c => c.Reservations)
                        .Where(r => r.DateReservation >= debut && r.DateReservation <= fin && r.Statut == "Confirmée")
                        .Sum(r => r.MontantTotal)
                })
                .ToDictionaryAsync(x => x.Nom, x => x.Ca);
            
            return new ChiffreAffairesDto
            {
                Total = total,
                Evolution = Math.Round(evolution, 2),
                ParMois = caParMois,
                ParTerrain = caParTerrain
            };
        }

        public async Task<List<TerrainOccupationDto>> GetTopTerrainsAsync(int top = 5)
        {
            return await _context.Terrains
                .Select(t => new TerrainOccupationDto
                {
                    TerrainId = t.Id,
                    TerrainNom = t.Nom,
                    NombreReservations = t.Creneaux
                        .SelectMany(c => c.Reservations)
                        .Count(r => r.Statut == "Confirmée"),
                    ChiffreAffaires = t.Creneaux
                        .SelectMany(c => c.Reservations)
                        .Where(r => r.Statut == "Confirmée")
                        .Sum(r => r.MontantTotal)
                })
                .OrderByDescending(t => t.NombreReservations)
                .Take(top)
                .ToListAsync();
        }

        public async Task<List<CreneauPopulariteDto>> GetCreneauxPopulairesAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Creneau)
                .Where(r => r.Statut == "Confirmée")
                .ToListAsync();
            
            var groupes = reservations
                .GroupBy(r => new { r.Creneau.HeureDebut, r.Creneau.HeureFin })
                .Select(g => new CreneauPopulariteDto
                {
                    HeureDebut = g.Key.HeureDebut,
                    HeureFin = g.Key.HeureFin,
                    NombreReservations = g.Count()
                })
                .OrderByDescending(c => c.NombreReservations)
                .ToList();
            
            var totalReservations = reservations.Count;
            foreach (var creneau in groupes)
            {
                creneau.TauxOccupation = totalReservations > 0 
                    ? (decimal)creneau.NombreReservations / totalReservations * 100 
                    : 0;
                creneau.TauxOccupation = Math.Round(creneau.TauxOccupation, 2);
            }
            
            return groupes.Take(5).ToList();
        }

        public async Task<byte[]> ExportRapportAsync(DateTime debut, DateTime fin)
        {
            var stats = await GetTauxOccupationAsync(debut, fin);
            var ca = await GetChiffreAffairesAsync(debut, fin);
            
            var rapport = $@"
                RAPPORT D'ACTIVITÉ
                Période : {debut:dd/MM/yyyy} - {fin:dd/MM/yyyy}
                
                CHIFFRE D'AFFAIRES
                Total : {ca.Total} DH
                Évolution : {ca.Evolution}%
                
                TAUX D'OCCUPATION GLOBAL
                {stats.TauxOccupationGlobal}%
                
                TOP 5 TERRAINS
                {string.Join("\n", stats.TerrainsOccupation.OrderByDescending(t => t.NombreReservations).Take(5)
                    .Select(t => $"- {t.TerrainNom}: {t.NombreReservations} réservations, {t.ChiffreAffaires} DH"))}
            ";
            
            return System.Text.Encoding.UTF8.GetBytes(rapport);
        }
    }
}