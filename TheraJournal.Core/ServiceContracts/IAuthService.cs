using TheraJournal.Core.DTO;

namespace TheraJournal.Core.ServiceContracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        Task<AuthenticationResponseDTO?>? LoginAsync(LoginDTO loginDTO);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        Task<AuthenticationResponseDTO?>? RegisterPatientAsync(RegisterPatientDTO registerDTO);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        Task<AuthenticationResponseDTO?>? RegisterTherapistAsync(RegisterTherapistDTO registerDTO);
       // Task<AuthenticationResponseDTO?> RefreshTokenAsync(string refreshToken);
       
    }
}
