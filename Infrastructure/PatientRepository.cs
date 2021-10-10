using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DomainServices;

namespace Infrastructure
{
    public class PatientRepository : IPatientRepository
    {
        private DbFysioContext _context { get; set; }

        public PatientRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public async Task AddPatient(Patient patient)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
        }

        public int CountPatients()
        {
            return _context.Patients.Count();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetWhereIdPatient(int id)
        {
            return _context.Patients.Find(id);
        }

        public void RemovePatient(Patient patient)
        {
            _context.Remove(patient);
            _context.SaveChanges();
        }

        public void UpdatePatient(Patient patient)
        {
            _context.Update(patient);
            _context.SaveChanges();
        }
    }
}
