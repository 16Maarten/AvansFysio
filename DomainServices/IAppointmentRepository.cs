using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IAppointmentRepository
    {
        Task AddAppointment(Appointment appointment);
        Task RemoveAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        public Appointment GetWhereIdAppointment(int id);
        public IEnumerable<Appointment> GetAllAppointments();
    }
}
