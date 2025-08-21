using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheraJournal.Core.Domain.Entities;

namespace TheraJournal.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Therapist? Therapist { get; set; }
        public Patient? Patient { get; set; }
        
    }
}
