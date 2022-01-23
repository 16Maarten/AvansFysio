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
    public class AppointmentRepository : IAppointmentRepository
    {
        private DbFysioContext _context { get; set; }

        public AppointmentRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }

        public async Task AddAppointment(Appointment appointment)
        {
            _context.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAppointment(Appointment appointment)
        {
            _context.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            _context.Update(appointment);
            await _context.SaveChangesAsync();
        }

        Appointment IAppointmentRepository.GetWhereIdAppointment(int id)
        {
            return _context.Appointments.FirstOrDefault(entity => entity.Id == id);
        }

        IEnumerable<Appointment> IAppointmentRepository.GetAllAppointments()
        {
            return _context.Appointments.Include(b => b.Patient).ToList();
        }
    }
}
