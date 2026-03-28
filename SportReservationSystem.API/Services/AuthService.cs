using Microsoft.EntityFrameworkCore;
using SportReservationSystem.API.Data;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SportReservationSystem.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Vérifier si l'email existe déjà
            var existingUser = await _context.Clients.FirstOrDefaultAsync(c => c.Email == registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Un compte existe déjà avec cet email"
                };
            }

            // Créer le nouvel utilisateur
            var client = new Client
            {
                Nom = registerDto.Nom,
                Prenom = registerDto.Prenom,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Telephone = registerDto.Telephone,
                Role = "Client",
                EstActif = true,
                DateInscription = DateTime.Now
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            // Générer le token JWT
            var token = GenerateJwtToken(client);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Message = "Inscription réussie",
                User = new UserDto
                {
                    Id = client.Id,
                    Nom = client.Nom,
                    Prenom = client.Prenom,
                    Email = client.Email,
                    Role = client.Role,
                    Telephone = client.Telephone
                }
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == loginDto.Email);
            
            if (client == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, client.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email ou mot de passe incorrect"
                };
            }

            if (!client.EstActif)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Votre compte est désactivé. Veuillez contacter l'administrateur."
                };
            }

            var token = GenerateJwtToken(client);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Message = "Connexion réussie",
                User = new UserDto
                {
                    Id = client.Id,
                    Nom = client.Nom,
                    Prenom = client.Prenom,
                    Email = client.Email,
                    Role = client.Role,
                    Telephone = client.Telephone
                }
            };
        }

        public async Task<AuthResponseDto> GetUserByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            
            if (client == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Utilisateur non trouvé"
                };
            }

            return new AuthResponseDto
            {
                Success = true,
                User = new UserDto
                {
                    Id = client.Id,
                    Nom = client.Nom,
                    Prenom = client.Prenom,
                    Email = client.Email,
                    Role = client.Role,
                    Telephone = client.Telephone
                }
            };
        }

        private string GenerateJwtToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim(ClaimTypes.GivenName, client.Prenom),
                new Claim(ClaimTypes.Surname, client.Nom),
                new Claim(ClaimTypes.Role, client.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}