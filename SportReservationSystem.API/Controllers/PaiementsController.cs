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
    public class PaiementsController : ControllerBase
    {
        private readonly IPaiementService _paiementService;

        public PaiementsController(IPaiementService paiementService)
        {
            _paiementService = paiementService;
        }

        private int GetCurrentClientId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        // GET: api/paiements/mes-paiements
        [HttpGet("mes-paiements")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetMesPaiements()
        {
            var clientId = GetCurrentClientId();
            var paiements = await _paiementService.GetByClientIdAsync(clientId);
            return Ok(paiements);
        }

        // GET: api/paiements/reservation/{reservationId}
        [HttpGet("reservation/{reservationId}")]
        public async Task<IActionResult> GetByReservationId(int reservationId)
        {
            var paiement = await _paiementService.GetByReservationIdAsync(reservationId);
            if (paiement == null)
                return NotFound();
            return Ok(paiement);
        }

        // POST: api/paiements
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody] CreatePaiementDto dto)
        {
            var paiement = await _paiementService.CreateAsync(dto);
            if (paiement == null)
                return BadRequest(new { message = "Réservation non trouvée ou déjà payée" });
            
            return CreatedAtAction(nameof(GetByReservationId), new { reservationId = paiement.ReservationId }, paiement);
        }

        // POST: api/paiements/{id}/refund
        [HttpPost("{id}/refund")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> Refund(int id)
        {
            var result = await _paiementService.RefundAsync(id);
            if (!result)
                return BadRequest(new { message = "Impossible de rembourser (délai dépassé ou paiement non trouvé)" });
            
            return Ok(new { message = "Remboursement effectué" });
        }

        // GET: api/paiements/{id}/invoice
        [HttpGet("{id}/invoice")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await _paiementService.GenerateInvoiceAsync(id);
            if (string.IsNullOrEmpty(invoice))
                return NotFound();
            
            return Ok(invoice);
        }
    }
}