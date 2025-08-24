using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AccountController(IAuthService authService,
            IJwtService jwtService) 
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("patient-register")]
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
                return Problem("Patient Registration Failed");
            }

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("therapist-register")]
        public async Task<ActionResult<ApplicationUser>> PostTherapistRegister(RegisterTherapistDTO registerDTO) 
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            AuthenticationResponseDTO result = await _authService.RegisterTherapistAsync(registerDTO);

            if (result == null)
            {
                return Problem("Therapist Registration Failed");
            }

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO) 
        {
            // TODO How to implement checking user role unless keeping separate login endpoints for each role?
             if (ModelState.IsValid == false)
             {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
             }

             // TODO create a service for checking user role
             
             

             return Ok("Lol");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            return NoContent();
        }
    }
}
