using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.Entities;

namespace TheraJournal.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRefreshTokenStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>

        Task AddAsync(RefreshToken rt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<RefreshToken?> GetByTokenAsync(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteAsync(string token);
    }
}
