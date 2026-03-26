using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;

namespace SportReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TerrainsController : ControllerBase
{
    private readonly ITerrainService _terrainService;

    public TerrainsController(ITerrainService terrainService)
    {
        _terrainService = terrainService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _terrainService.GetAllAsync());
    }
}
