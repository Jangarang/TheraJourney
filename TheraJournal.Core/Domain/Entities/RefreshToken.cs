using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheraJournal.Core.Domain.IdentityEntities;

namespace TheraJournal.Core.Domain.Entities
{

    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } //

        public DateTime CreateAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool isUsed { get; set; }
        public bool isRevoked { get; set; }
    }
}
