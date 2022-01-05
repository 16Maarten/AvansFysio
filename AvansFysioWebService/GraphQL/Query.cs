using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioWebService.GraphQL
{
    public class Query
    {
        private readonly IVektisRepository _vektisRepository;

        public Query(IVektisRepository vektisRepository)
        {
            _vektisRepository = vektisRepository;
        }

        public VektisDiagnosis GetDiagnosisById(int code) => _vektisRepository.GetDiagnosisByCode(code);
        public IEnumerable<VektisDiagnosis> Diagnoses => _vektisRepository.GetAllDiagnoses();
        public VektisTreatment GetTreatmentById(string code) => _vektisRepository.GetTreatmentByCode(code);
        public IEnumerable<VektisTreatment> Operations => _vektisRepository.GetAllTreatments();
    }
}
