using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services;

public class TerrainService : ITerrainService
{
    private readonly ApplicationDbContext _dbContext;

    public TerrainService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Terrain>> GetAllAsync()
    {
        return _dbContext.Terrains.AsNoTracking().OrderBy(t => t.Name).ToListAsync();
    }
}
