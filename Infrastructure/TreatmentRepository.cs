using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private DbFysioContext _context { get; set; }

        public TreatmentRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public async Task AddTreatment(Treatment treatment)
        {
            _context.Add(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTreatment(Treatment treatment)
        {
            _context.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTreatment(Treatment treatment)
        {
            _context.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public Treatment GetWhereIdTreatment(int id)
        {
            return _context.Treatments.Include(b => b.Student).Include(b => b.Physiotherapist).FirstOrDefault(entity => entity.Id == id); ;
        }

        public IEnumerable<Treatment> GetAllTreatments()
        {
            return _context.Treatments.ToList();
        }
    }
}
