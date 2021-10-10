
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IPatientRepository
    {
        Task AddPatient(Patient patient);
        public void RemovePatient(Patient patient);
        public void UpdatePatient(Patient patient);
        public Patient GetWhereIdPatient(int id);
        public IEnumerable<Patient> GetAllPatients();
        public int CountPatients();
    }
}
