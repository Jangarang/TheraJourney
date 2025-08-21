using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.IdentityEntities;

namespace TheraJournal.Core.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!; // Why is this here?
        public string? PatientName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        // foreign key for therapist
        public Guid? TherapistId { get; set; }
        public Therapist? Therapist { get; set; } // nav property. What is this?


    }
}
