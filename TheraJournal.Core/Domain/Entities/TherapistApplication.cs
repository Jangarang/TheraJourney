using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheraJournal.Core.Domain.Entities
{
    public class TherapistApplication
    {
        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
    }
}
