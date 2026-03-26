using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _reservationService.GetAllAsync());
    }

    [HttpGet("client/{clientId:int}")]
    public async Task<IActionResult> GetByClient(int clientId)
    {
        return Ok(await _reservationService.GetByClientAsync(clientId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationDto dto)
    {
        var reservation = await _reservationService.CreateAsync(dto);
        if (reservation is null)
        {
            return BadRequest("Creneau unavailable.");
        }

        return Ok(reservation);
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var updated = await _reservationService.CancelAsync(id);
        return updated ? Ok() : NotFound();
    }

    [HttpPut("{id:int}/validate")]
    [Authorize(Roles = "Gestionnaire")]
    public async Task<IActionResult> Validate(int id)
    {
        var updated = await _reservationService.ValidateAsync(id);
        return updated ? Ok() : NotFound();
    }
}
