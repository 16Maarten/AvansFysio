using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PhysiotherapistRepository : IPhysiotherapistRepository
    {
        private DbFysioContext _context { get; set; }

        public PhysiotherapistRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public int CountPhysiotherapists()
        {
            return _context.Physiotherapists.Count();
        }

        public IEnumerable<Physiotherapist> GetAllPhysiotherapists()
        {
            return _context.Physiotherapists.Include(b => b.Presence).ToList();
        }

        public IQueryable GetPhysiotherapists()
        {
            return _context.Physiotherapists;
        }

        public Physiotherapist GetWhereIdPhysiotherapist(int id)
        {
            return _context.Physiotherapists.Include(b => b.Presence).FirstOrDefault(entity => entity.Id == id);
        }

        public Physiotherapist GetWhereEmailPhysiotherapist(string email)
        {
            return _context.Physiotherapists.Include(b => b.Presence).FirstOrDefault(entity => entity.Email == email);
        }
    }
}
