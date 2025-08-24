using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.Entities;

namespace TheraJournal.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for mapping Therapist Entity.
    /// </summary>
    public interface ITherapistRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task AddAsync(Therapist therapist);
    }
}
