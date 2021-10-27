﻿using Domain;
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
        public PatientFile GetWhereIdPatientFile(int id);
        public IEnumerable<PatientFile> GetAllPatientFiles();
    }
}
