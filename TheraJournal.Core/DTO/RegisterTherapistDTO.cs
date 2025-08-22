using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheraJournal.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new therapist.
    /// </summary>
    public class RegisterTherapistDTO
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PersonName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Therapist specific
        public string LicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
    }
}
