using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrainsController : ControllerBase
    {
        private readonly ITerrainService _terrainService;

        public TerrainsController(ITerrainService terrainService)
        {
            _terrainService = terrainService;
        }

        // GET: api/terrains
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var terrains = await _terrainService.GetAllAsync();
            return Ok(terrains);
        }

        // GET: api/terrains/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var terrain = await _terrainService.GetByIdAsync(id);
            if (terrain == null)
                return NotFound();
            return Ok(terrain);
        }

        // POST: api/terrains
        [HttpPost]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Create([FromBody] Terrain terrain)
        {
            var created = await _terrainService.CreateAsync(terrain);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/terrains/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Update(int id, [FromBody] Terrain terrain)
        {
            var updated = await _terrainService.UpdateAsync(id, terrain);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: api/terrains/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _terrainService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}