using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Core.DTO;
using TheraJournal.Core.ServiceContracts;

namespace TheraJournal.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IRefreshTokenStore _refreshTokenRepo;

        private readonly IPatientRepository _patientRepository;
        private readonly ITherapistRepository _therapistRepository;
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IPatientRepository patientRepository,
            IAuthService authService,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _patientRepository = patientRepository;
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

                // Create Tokens
                var authenticationResponse = _jwtService.CreateJwtToken(user);

                RefreshToken refreshToken = new RefreshToken (
                     
                );

                await _userManager.UpdateAsync(user);

                return authenticationResponse;
            }

            return null;
        }

        public async Task<AuthenticationResponseDTO?>? RegisterPatientAsync(RegisterPatientDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            //if (registerDTO.Role == Enums.UserRole.Patient)
            //{
            await _patientRepository.AddAsync(new Patient
            {
                ApplicationUserId = user.Id,
                PatientName = registerDTO.PersonName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                Gender = registerDTO.Gender,
                Address = registerDTO.Address,
                DateOfBirth = registerDTO.DateOfBirth,
            });

            await _userManager.AddToRoleAsync(user, ApplicationRole.Patient.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);

            var authenticationResponse = _jwtService.CreateJwtToken(user); // create jwttoken
                       
            //user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
             
            
            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }
        
        public Task<AuthenticationResponseDTO?>? RegisterTherapistAsync(RegisterTherapistDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
