using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreneauxController : ControllerBase
    {
        private readonly ICreneauService _creneauService;

        public CreneauxController(ICreneauService creneauService)
        {
            _creneauService = creneauService;
        }

        // GET: api/creneaux
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var creneaux = await _creneauService.GetAllAsync();
            return Ok(creneaux);
        }

        // GET: api/creneaux/disponibles?terrainId=1&date=2025-03-27
        [HttpGet("disponibles")]
        [Authorize]
        public async Task<IActionResult> GetDisponibles([FromQuery] int terrainId, [FromQuery] DateTime date)
        {
            var creneaux = await _creneauService.GetDisponiblesAsync(terrainId, date);
            return Ok(creneaux);
        }

        // GET: api/creneaux/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var creneau = await _creneauService.GetByIdAsync(id);
            if (creneau == null)
                return NotFound();
            return Ok(creneau);
        }

        // POST: api/creneaux
        [HttpPost]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Create([FromBody] Creneau creneau)
        {
            var created = await _creneauService.CreateAsync(creneau);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}