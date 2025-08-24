using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.DTO;
using TheraJournal.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.RepositoryContracts;

namespace TheraJournal.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenStore _refreshTokenRepo;

        public JwtService(IConfiguration configuration,
            IRefreshTokenStore refreshTokenRepo) 
        { 
            _configuration = configuration;
            _refreshTokenRepo = refreshTokenRepo;
        }

        /// <summary>
        /// Generates a JWT token using the given user's information and the configuration settings.
        /// </summary>
        /// <param name="user">ApplicationUser object</param>
        /// <returns>AuthenticationResponse that includes token</returns>
        public async Task<AuthenticationResponseDTO?>? CreateJwtToken(ApplicationUser user)
        {
            // Create a DateTime object representing the token expiration time by adding the number of minutes specified in the configuration to the current UTC time.
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
            Claim[] claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //Subject (user id)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT unique ID
                new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),

                new Claim(ClaimTypes.NameIdentifier, user.Email), //Unique name identifier of the user (Email)
                //new Claim(ClaimTypes.Name, user.PersonName), //Name of the user
                new Claim(ClaimTypes.Email, user.Email) //Name of the user
            };

            // Create a SymmetricSecurityKey object using the key specified in the configuration.
            //SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") 
                           ?? throw new InvalidOperationException("JWT Key not configured"))
             );

            // Create a SigningCredentials object with the security key and the HMACSHA256 algorithm.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a JwtSecurityToken object with the given issuer, audience, claims, expiration, and signing credentials.
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            // Create a JwtSecurityTokenHandler object and use it to write the token as a string.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            await CreateRefreshToken(token, user);

            // Create and return an AuthenticationResponse object containing the token, user email, user name, and token expiration time.
            return new AuthenticationResponseDTO()
            {
                Token = token,
                Email = user.Email,
                Expiration = expiration,
            };
        }
        
        //Creates a refresh token (base 64 string of random numbers)
        private async Task CreateRefreshToken(string token, ApplicationUser user)
        {
            //Byte[] bytes = new byte[64];
                
            //RandomNumberGenerator.Create();
            //var randomNumberGenerator = RandomNumberGenerator.Create();
            //randomNumberGenerator.GetBytes(bytes);
            
            RefreshToken rt = new RefreshToken()
            {
                Token = token,
                //JwtId = Guid.NewGuid().ToString(),
                User = user,
                CreateAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                isUsed = true,
                isRevoked = false
            };

            await _refreshTokenRepo.AddAsync(rt);

            return;
            //return Convert.ToBase64String(bytes);
        }

        //public ClaimsPrincipal GetPrincipalFromJwtToken(string? token)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
