using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Services.Interfaces;

public interface IAuthService
{
    Task<(string Token, string Role, int ClientId)?> LoginAsync(LoginDto dto);
}
