using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.IdentityEntities;

namespace TheraJournal.Core.Domain.Entities
{
    public class Therapist
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string TherapistName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
      
        public ApplicationUser ApplicationUser { get; set; } = null!; //Why is this here?

        //Domain Specific fields
        public string LicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

        // Relationships
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    }
}
