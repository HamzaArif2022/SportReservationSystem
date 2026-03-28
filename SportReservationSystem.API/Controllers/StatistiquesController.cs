using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;

namespace SportReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Gestionnaire")]
    public class StatistiquesController : ControllerBase
    {
        private readonly IStatistiqueService _statistiqueService;

        public StatistiquesController(IStatistiqueService statistiqueService)
        {
            _statistiqueService = statistiqueService;
        }

        // GET: api/statistiques/dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var stats = await _statistiqueService.GetDashboardStatsAsync();
            return Ok(stats);
        }

        // GET: api/statistiques/occupation
        [HttpGet("occupation")]
        public async Task<IActionResult> GetTauxOccupation([FromQuery] DateTime? debut, [FromQuery] DateTime? fin)
        {
            var stats = await _statistiqueService.GetTauxOccupationAsync(debut, fin);
            return Ok(stats);
        }

        // GET: api/statistiques/chiffre-affaires
        [HttpGet("chiffre-affaires")]
        public async Task<IActionResult> GetChiffreAffaires([FromQuery] DateTime? debut, [FromQuery] DateTime? fin)
        {
            var stats = await _statistiqueService.GetChiffreAffairesAsync(debut, fin);
            return Ok(stats);
        }

        // GET: api/statistiques/top-terrains
        [HttpGet("top-terrains")]
        public async Task<IActionResult> GetTopTerrains([FromQuery] int top = 5)
        {
            var stats = await _statistiqueService.GetTopTerrainsAsync(top);
            return Ok(stats);
        }

        // GET: api/statistiques/creneaux-populaires
        [HttpGet("creneaux-populaires")]
        public async Task<IActionResult> GetCreneauxPopulaires()
        {
            var stats = await _statistiqueService.GetCreneauxPopulairesAsync();
            return Ok(stats);
        }

        // GET: api/statistiques/export
        [HttpGet("export")]
        public async Task<IActionResult> ExportRapport([FromQuery] DateTime debut, [FromQuery] DateTime fin)
        {
            var rapport = await _statistiqueService.ExportRapportAsync(debut, fin);
            return File(rapport, "text/plain", $"rapport_{debut:yyyyMMdd}_{fin:yyyyMMdd}.txt");
        }
    }
}