using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface ITreatmentPlanRepository
    {
        Task AddTreatmentPlan(TreatmentPlan treatmentPlan);
        Task RemoveTreatmentPlan(TreatmentPlan treatmentPlan);
        Task UpdateTreatmentPlan(TreatmentPlan treatmentPlan);
        public TreatmentPlan GetWhereIdTreatmentPlan(int id);
        public IEnumerable<TreatmentPlan> GetAllTreatmentPlans();
    }
}
