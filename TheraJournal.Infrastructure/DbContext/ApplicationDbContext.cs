using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheraJournal.Core.Domain.Entities;
using TheraJournal.Core.Domain.IdentityEntities;

namespace TheraJournal.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        //Dbset
        public virtual DbSet<Therapist> Therapists { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        
        public virtual DbSet<TherapistApplication> TherapistApplications { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        public ApplicationDbContext() { }

        //seed data possibly?

    }
}
