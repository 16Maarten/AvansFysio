using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PresenceRepository : IPresenceRepository
    {
        private DbFysioContext _context { get; set; }

        public PresenceRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public Presence GetWhereIdPresence(int id)
        {
            return _context.Presences.FirstOrDefault(entity => entity.Id == id);
        }

        public async Task UpdatePresence(Presence presence)
        {
            _context.Update(presence);
            await _context.SaveChangesAsync();
        }
    }
}
