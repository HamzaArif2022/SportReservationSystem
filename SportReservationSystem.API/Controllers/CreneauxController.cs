using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;

namespace SportReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CreneauxController : ControllerBase
{
    private readonly ICreneauService _creneauService;

    public CreneauxController(ICreneauService creneauService)
    {
        _creneauService = creneauService;
    }

    [HttpGet("terrain/{terrainId:int}")]
    public async Task<IActionResult> GetByTerrain(int terrainId)
    {
        return Ok(await _creneauService.GetByTerrainAsync(terrainId));
    }

    [HttpGet("available")]
    public async Task<IActionResult> SearchAvailable([FromQuery] DateTime? date)
    {
        return Ok(await _creneauService.SearchAvailableAsync(date));
    }
}
