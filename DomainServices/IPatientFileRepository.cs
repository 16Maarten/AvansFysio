using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IPatientFileRepository
    {
        Task AddPatientFile(PatientFile patientFile);
        Task UpdatePatientFile(PatientFile patientFile);
        Task RemovePatientFile(PatientFile patientFile);
        public PatientFile GetWherePatientIdPatientFile(int id);
        public PatientFile GetWhereIdPatientFile(int id);
        public IEnumerable<PatientFile> GetAllPatientFiles();
    }
}
