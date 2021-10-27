using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface ITreatmentRepository
    {
        Task AddTreatment(Treatment treatment);
        Task RemoveTreatment(Treatment treatment);
        Task UpdateTreatment(Treatment treatment);
        public Treatment GetWhereIdTreatment(int id);
        public IEnumerable<Treatment> GetAllTreatments();
    }
}
