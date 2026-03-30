using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> GetUserByIdAsync(int id);
    }
}