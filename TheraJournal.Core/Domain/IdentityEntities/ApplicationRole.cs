using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheraJournal.Core.Domain.IdentityEntities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public const string Patient = "Patient";
        public const string Therapist = "Therapist";
        public const string PendingTherapist = "PendingTherapist";
    }
}
