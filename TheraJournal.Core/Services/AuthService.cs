using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.DTO;
using TheraJournal.Core.ServiceContracts;

namespace TheraJournal.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthService authService,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        // TODO: Implement validation checks with message sent to the client.
        public async Task<AuthenticationResponseDTO?>? LoginAsync(LoginDTO loginDTO)
        {
            var result = _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.IsCompletedSuccessfully)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null) {
                    return null;
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                var authenticationResponse = _jwtService.CreateJwtToken(user);
                await _userManager.UpdateAsync(user);

                return authenticationResponse;
            }

            return null;
        }

        public Task<AuthenticationResponseDTO?> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
            //ApplicationUser user = new ApplicationUser
            //{
            //    Email = registerDTO.Email,
            //    PhoneNumber = registerDTO.PhoneNumber,
            //    UserName = registerDTO.Email,
            //};
        }
    }
}
