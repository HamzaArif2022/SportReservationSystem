using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services
{
    public class TerrainService : ITerrainService
    {
        private readonly ApplicationDbContext _context;

        public TerrainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Terrain>> GetAllAsync()
        {
            return await _context.Terrains.ToListAsync();
        }

        public async Task<Terrain?> GetByIdAsync(int id)
        {
            return await _context.Terrains.FindAsync(id);
        }

        public async Task<Terrain> CreateAsync(Terrain terrain)
        {
            _context.Terrains.Add(terrain);
            await _context.SaveChangesAsync();
            return terrain;
        }

        public async Task<Terrain?> UpdateAsync(int id, Terrain terrain)
        {
            var existing = await _context.Terrains.FindAsync(id);
            if (existing == null) return null;
            
            existing.Nom = terrain.Nom;
            existing.TypeSport = terrain.TypeSport;
            existing.TarifHoraire = terrain.TarifHoraire;
            existing.Capacite = terrain.Capacite;
            existing.Statut = terrain.Statut;
            
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var terrain = await _context.Terrains.FindAsync(id);
            if (terrain == null) return false;
            
            _context.Terrains.Remove(terrain);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Terrain>> GetDisponiblesAsync()
        {
            return await _context.Terrains
                .Where(t => t.Statut == "Disponible")
                .ToListAsync();
        }
    }
}