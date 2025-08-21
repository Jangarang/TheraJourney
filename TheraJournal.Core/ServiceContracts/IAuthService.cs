using TheraJournal.Core.DTO;

namespace TheraJournal.Core.ServiceContracts
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDTO?>? LoginAsync(LoginDTO loginDTO);
        Task<AuthenticationResponseDTO?>? RegisterAsync(RegisterDTO registerDTO);
       // Task<AuthenticationResponseDTO?> RefreshTokenAsync(string refreshToken);
       
    }
}
