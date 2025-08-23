using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.RepositoryContracts;
using TheraJournal.Infrastructure.DbContext;

namespace TheraJournal.Infrastructure.Repositories
{
  
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
           
        } 

        public async Task AddAsync(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync(); 
        }
    }
}
