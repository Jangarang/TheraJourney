using Microsoft.AspNetCore.Identity;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.IdentityEntities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Core.DTO;
using TheraJournal.Core.Enums;
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

        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IPatientRepository patientRepository,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _patientRepository = patientRepository;
            _jwtService = jwtService;
        }
        
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

                await _userManager.UpdateAsync(user);

                return await authenticationResponse;
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

            //Create 'Patient role if it doesnt exist
            if (await _roleManager.FindByNameAsync(UserRole.Patient.ToString()) is null) 
            { 
                ApplicationRole applicationRole = new ApplicationRole()
                { 
                    Name = UserRole.Patient.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
            }
            // Add the patient into the 'Patient' role
            await _userManager.AddToRoleAsync(user, UserRole.Patient.ToString());
            //await _userManager.AddToRoleAsync(user, ApplicationRole.Patient.ToString());

            await _signInManager.SignInAsync(user, isPersistent: false);

            AuthenticationResponseDTO authenticationResponse = await _jwtService.CreateJwtToken(user); // create jwttoken
                       
            //user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
             
            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }

        public async Task<AuthenticationResponseDTO?>? RegisterTherapistAsync(RegisterTherapistDTO registerDTO)
        {
            // throw new NotImplementedException();
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

            await _therapistRepository.AddAsync(new Therapist
            {
                ApplicationUserId = user.Id,
                TherapistName = registerDTO.PersonName,
                Email = registerDTO.Email,
                Gender = registerDTO.Gender,
                PhoneNumber = registerDTO.PhoneNumber,
                Status = TherapistStatus.Pending,
            });

            TherapistApplication therapistApp = new TherapistApplication
            {
                ApplicationUserId = user.Id,
                LicenseNumber = registerDTO.LicenseNumber,
                SubmittedAt = DateTime.UtcNow,
                IsApproved = false,
            };

            //Create Therapist role if it doesn't exist
            if (await _roleManager.FindByNameAsync(UserRole.Therapist.ToString()) is null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = UserRole.Patient.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
            }

            // Add the therapist into the 'Therapist' role
            await _userManager.AddToRoleAsync(user, UserRole.Therapist.ToString());

            await _signInManager.SignInAsync(user, isPersistent:false);

            AuthenticationResponseDTO authenticationResponse = await _jwtService.CreateJwtToken(user); // create jwttoken

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }

        //public async Task<> get()
    }
}
