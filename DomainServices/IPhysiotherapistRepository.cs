using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IPhysiotherapistRepository
    {
        public Physiotherapist GetWhereIdPhysiotherapist(int id);
        public IEnumerable<Physiotherapist> GetAllPhysiotherapists();
        public IQueryable GetPhysiotherapists();
        public int CountPhysiotherapists();
        public Physiotherapist GetWhereEmailPhysiotherapist(string email);
    }
}
