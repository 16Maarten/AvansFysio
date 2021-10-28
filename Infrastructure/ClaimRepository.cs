using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly DbSecurityContext _context;
        public ClaimRepository(DbSecurityContext context)
        {
            this._context = context;
        }

        public string GetClaim(string userName)
        {
            return (from user in _context.Users
                    join claim in _context.UserClaims
                    on user.Id equals claim.UserId
                    where user.UserName == userName
                    select claim.ClaimType).SingleOrDefault();
        }
    }
}
