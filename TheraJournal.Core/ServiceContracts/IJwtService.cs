using System.Security.Claims;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.DTO;

namespace TheraJournal.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponseDTO CreateJwtToken(ApplicationUser user);
        //ClaimsPrincipal GetPrincipalFromJwtToken(string? token);
        Task<string> CreateRefreshToken();
    }
}
