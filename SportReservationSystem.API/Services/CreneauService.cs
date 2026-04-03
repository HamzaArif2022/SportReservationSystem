using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services
{
    public class CreneauService : ICreneauService
    {
        private readonly ApplicationDbContext _context;

        public CreneauService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CreneauDto>> GetDisponiblesAsync(int terrainId, DateTime date)
        {
            return await _context.Creneaux
                .Where(c => c.Date == date.Date
                         && (terrainId == 0 || c.TerrainId == terrainId)
                         && c.EstDisponible)
                .OrderBy(c => c.HeureDebut)
                .Select(c => new CreneauDto
                {
                    Id = c.Id,
                    TerrainId = c.TerrainId,
                    Date = c.Date,
                    HeureDebut = c.HeureDebut,
                    HeureFin = c.HeureFin,
                    EstDisponible = c.EstDisponible,

                    // infos du Terrain
                    TerrainNom = c.Terrain.Nom,
                    Capacite = c.Terrain.Capacite,
                    TarifHoraire = c.Terrain.TarifHoraire,
                    TypeSport = c.Terrain.TypeSport
                })
                .ToListAsync();
        }

        public async Task<CreneauDto?> GetByIdAsync(int id)
        {
            return await _context.Creneaux
                .Where(c => c.Id == id)
                .Select(c => new CreneauDto
                {
                    Id = c.Id,
                    TerrainId = c.TerrainId,
                    Date = c.Date,
                    HeureDebut = c.HeureDebut,
                    HeureFin = c.HeureFin,
                    EstDisponible = c.EstDisponible,

                    // infos du Terrain
                    TerrainNom = c.Terrain.Nom,
                    Capacite = c.Terrain.Capacite,
                    TarifHoraire = c.Terrain.TarifHoraire,
                    TypeSport = c.Terrain.TypeSport
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Creneau> CreateAsync(Creneau creneau)
        {
            _context.Creneaux.Add(creneau);
            await _context.SaveChangesAsync();
            return creneau;
        }

        public async Task<List<CreneauDto>> GetAllAsync()
        {
            return await _context.Creneaux
                .OrderBy(c => c.Date)
                .ThenBy(c => c.HeureDebut)
                .Select(c => new CreneauDto
                {
                    Id = c.Id,
                    TerrainId = c.TerrainId,
                    Date = c.Date,
                    HeureDebut = c.HeureDebut,
                    HeureFin = c.HeureFin,
                    EstDisponible = c.EstDisponible,

                    // infos du Terrain
                    TerrainNom = c.Terrain.Nom,
                    Capacite = c.Terrain.Capacite,
                    TarifHoraire = c.Terrain.TarifHoraire,
                    TypeSport = c.Terrain.TypeSport
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateDisponibiliteAsync(int id, bool estDisponible)
        {
            var creneau = await _context.Creneaux.FindAsync(id);
            if (creneau == null) return false;

            creneau.EstDisponible = estDisponible;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}