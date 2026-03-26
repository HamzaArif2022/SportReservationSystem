using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Services;

public class CreneauService : ICreneauService
{
    private readonly ApplicationDbContext _dbContext;

    public CreneauService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Creneau>> GetByTerrainAsync(int terrainId)
    {
        return _dbContext.Creneaux
            .AsNoTracking()
            .Where(c => c.TerrainId == terrainId)
            .OrderBy(c => c.StartTime)
            .ToListAsync();
    }

    public Task<List<Creneau>> SearchAvailableAsync(DateTime? date)
    {
        var query = _dbContext.Creneaux
            .AsNoTracking()
            .Include(c => c.Terrain)
            .Where(c => c.IsAvailable);

        if (date.HasValue)
        {
            var day = date.Value.Date;
            query = query.Where(c => c.StartTime.Date == day);
        }

        return query.OrderBy(c => c.StartTime).ToListAsync();
    }
}
