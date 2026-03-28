using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using System.Security.Claims;

namespace SportReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private int GetCurrentClientId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        // GET: api/notifications/mes-notifications
        [HttpGet("mes-notifications")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetMesNotifications()
        {
            var clientId = GetCurrentClientId();
            var notifications = await _notificationService.GetByClientIdAsync(clientId);
            return Ok(notifications);
        }

        // GET: api/notifications/non-lues
        [HttpGet("non-lues")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetNonLues()
        {
            var clientId = GetCurrentClientId();
            var notifications = await _notificationService.GetUnreadByClientIdAsync(clientId);
            return Ok(notifications);
        }

        // PUT: api/notifications/{id}/read
        [HttpPut("{id}/read")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return NoContent();
        }

        // POST: api/notifications/reminder/{reservationId}
        [HttpPost("reminder/{reservationId}")]
        [Authorize(Roles = "Gestionnaire")]
        public async Task<IActionResult> SendReminder(int reservationId)
        {
            await _notificationService.SendSmsReminderAsync(reservationId);
            return Ok(new { message = "Rappel envoyé" });
        }
    }
}