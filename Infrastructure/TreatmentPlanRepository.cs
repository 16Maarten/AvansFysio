using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TreatmentPlanRepository : ITreatmentPlanRepository
    {
        private DbFysioContext _context { get; set; }

        public TreatmentPlanRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public async Task AddTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            _context.Add(treatmentPlan);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TreatmentPlan> GetAllTreatmentPlans()
        {
            throw new NotImplementedException();
        }

        public TreatmentPlan GetWhereIdTreatmentPlan(int id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            _context.Remove(treatmentPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            _context.Update(treatmentPlan);
            await _context.SaveChangesAsync();
        }
    }
}
