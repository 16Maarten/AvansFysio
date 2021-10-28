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
    public class PatientFileRepository : IPatientFileRepository
    {

        private DbFysioContext _context { get; set; }

        public PatientFileRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }

        public async Task AddPatientFile(PatientFile patientFile)
        {
            _context.Add(patientFile);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PatientFile> GetAllPatientFiles()
        {
            return _context.PatientFiles.Include(b => b.Patient).Include(b => b.Student).Include(b => b.Physiotherapist).Include(b => b.TreatmentPlan).Include(b => b.Remarks).ToList();
        }

        public PatientFile GetWhereIdPatientFile(int id)
        {
            return _context.PatientFiles.Include(b => b.TreatmentPlan).Include(b => b.Treatments).Include(b => b.Patient).Include(b => b.Remarks).Include(b => b.Physiotherapist).Include(b => b.Student).FirstOrDefault(entity => entity.Id == id);
        }

        public async Task UpdatePatientFile(PatientFile patientFile)
        {
            _context.Update(patientFile);
            await _context.SaveChangesAsync();
        }

    }
}
