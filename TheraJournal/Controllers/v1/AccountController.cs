using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.DTO;
using TheraJournal.Core.ServiceContracts;

namespace TheraJournal.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AccountController(IJwtService jwtService) 
        {
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> PostPatientRegister(RegisterPatientDTO registerDTO) 
        {
            if (ModelState.IsValid == false) 
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            AuthenticationResponseDTO result = await _authService.RegisterPatientAsync(registerDTO);

            if (result == null) 
            {
                return Problem("Registration Failed");
            }

            return Ok(result);
        }
    }
}
