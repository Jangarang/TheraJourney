using Microsoft.EntityFrameworkCore;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Infrastructure.DbContext;

namespace TheraJournal.Infrastructure.Repositories
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly ApplicationDbContext _context;
        public RefreshTokenStore(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(RefreshToken rt)
        {
            await _context.RefreshTokens.AddAsync(rt);
        }

        public async Task DeleteAsync(string token)
        {
            var rt = await GetByTokenAsync(token);
            if (rt != null) 
            {
                _context.RefreshTokens.Remove(rt);
            }
        }
    }
}
