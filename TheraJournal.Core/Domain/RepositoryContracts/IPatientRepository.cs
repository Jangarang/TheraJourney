using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.Entities;

namespace TheraJournal.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for mapping Patient Entity.
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        ///  Adds a new patient object to the data store
        /// </summary>
        /// <param name="patient">Patient object to add</param>
        /// <returns></returns>
        Task AddAsync(Patient patient);
    }
}
