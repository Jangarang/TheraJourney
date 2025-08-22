using TheraJournal.Core.DTO;

namespace TheraJournal.Core.ServiceContracts
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDTO?>? LoginAsync(LoginDTO loginDTO);
        Task<AuthenticationResponseDTO?>? RegisterPatientAsync(RegisterPatientDTO registerDTO);

        Task<AuthenticationResponseDTO?>? RegisterTherapistAsync(RegisterTherapistDTO registerDTO);
       // Task<AuthenticationResponseDTO?> RefreshTokenAsync(string refreshToken);
       
    }
}
