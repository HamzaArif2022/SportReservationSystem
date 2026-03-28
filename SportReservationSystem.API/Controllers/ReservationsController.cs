using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using System.Security.Claims;

namespace SportReservationSystem.API.Controllers
{
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

        private int GetCurrentClientId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        // GET: api/reservations
        [HttpGet]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        // GET: api/reservations/en-attente
        [HttpGet("en-attente")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> GetEnAttente()
        {
            var reservations = await _reservationService.GetEnAttenteAsync();
            return Ok(reservations);
        }

        // GET: api/reservations/mes-reservations
        [HttpGet("mes-reservations")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetMesReservations()
        {
            var clientId = GetCurrentClientId();
            var reservations = await _reservationService.GetByClientIdAsync(clientId);
            return Ok(reservations);
        }

        // GET: api/reservations/planning
        [HttpGet("planning")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> GetPlanning([FromQuery] DateTime date)
        {
            var reservations = await _reservationService.GetPlanningAsync(date);
            return Ok(reservations);
        }

        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        // POST: api/reservations
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            var clientId = GetCurrentClientId();
            var reservation = await _reservationService.CreateAsync(dto, clientId);
            
            if (reservation == null)
                return BadRequest(new { message = "Créneau non disponible ou inexistant" });
            
            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        // PUT: api/reservations/{id}/validate
        [HttpPut("{id}/validate")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Validate(int id)
        {
            var reservation = await _reservationService.ValidateAsync(id);
            
            if (reservation == null)
                return NotFound(new { message = "Réservation non trouvée ou déjà traitée" });
            
            return Ok(reservation);
        }

        // PUT: api/reservations/{id}/refuse
        [HttpPut("{id}/refuse")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Refuse(int id, [FromBody] string motif)
        {
            if (string.IsNullOrWhiteSpace(motif))
                return BadRequest(new { message = "Motif de refus requis" });
            
            var reservation = await _reservationService.RefuseAsync(id, motif);
            
            if (reservation == null)
                return NotFound(new { message = "Réservation non trouvée ou déjà traitée" });
            
            return Ok(reservation);
        }

        // DELETE: api/reservations/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Cancel(int id)
        {
            var clientId = GetCurrentClientId();
            var result = await _reservationService.CancelAsync(id, clientId);
            
            if (!result)
                return BadRequest(new { message = "Impossible d'annuler la réservation (délai dépassé ou réservation non trouvée)" });
            
            return NoContent();
        }
    }
}