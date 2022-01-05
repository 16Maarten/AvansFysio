using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class VektisRepository : IVektisRepository
    {
        private readonly DbVektisContext _context;
        public VektisRepository(DbVektisContext context)
        {
            _context = context;
        }

        public IEnumerable<VektisDiagnosis> GetAllDiagnoses()
        {
            return _context.Diagnoses.ToList();
        }

        public VektisDiagnosis GetDiagnosisByCode(int code)
        {
            return _context.Diagnoses.Where(b => b.Code == code).SingleOrDefault();
        }

        public VektisTreatment GetTreatmentByCode(string code)
        {
            return _context.Treatments.Where(b => b.Code == code).SingleOrDefault();
        }

        public IEnumerable<VektisTreatment> GetAllTreatments()
        {
            return _context.Treatments.ToList();
        }
    }
}
