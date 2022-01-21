using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IPresenceRepository
    {
        public Presence GetWhereIdPresence(int id);
        Task UpdatePresence(Presence presence);
    }
}
