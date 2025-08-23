using Microsoft.AspNetCore.Mvc;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.ServiceContracts;

namespace TheraJournal.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        public AccountController(IJwtService jwtService) 
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostRegister() 
        {
            if (ModelState.IsValid == false) 
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

        }
    }
}
