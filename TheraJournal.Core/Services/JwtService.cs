using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.DTO;
using TheraJournal.Core.ServiceContracts;

namespace TheraJournal.Core.Services
{
    public class JwtService : IJwtService
    {
        public JwtService() { }
        public AuthenticationResponseDTO CreateJwtToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromJwtToken(string? token)
        {
            throw new NotImplementedException();
        }
    }
}
