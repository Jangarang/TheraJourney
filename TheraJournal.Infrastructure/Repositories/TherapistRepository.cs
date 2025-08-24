using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Infrastructure.DbContext;

namespace TheraJournal.Infrastructure.Repositories
{
    public class TherapistRepository : ITherapistRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TherapistRepository() 
        {
            
        }

        public Task AddAsync(Therapist therapist)
        {
            _dbContext.Therapists.Add(therapist);
            return _dbContext.SaveChangesAsync();
        }
    }
}
