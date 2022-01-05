using System;
using System.Collections.Generic;
using Domain;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IVektisRepository
    {
        VektisDiagnosis GetDiagnosisByCode(int code);
        IEnumerable<VektisDiagnosis> GetAllDiagnoses();
        VektisTreatment GetTreatmentByCode(string code);
        IEnumerable<VektisTreatment> GetAllTreatments();
    }
}
