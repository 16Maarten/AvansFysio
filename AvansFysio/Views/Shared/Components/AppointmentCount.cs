using DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Views.Components
{
    public class AppointmentCount : ViewComponent
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentCount(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public string Invoke()
        {
            int numberOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => (p.Physiotherapist.Email == User.Identity.Name || p.Student.Email == User.Identity.Name) && p.Date.Date == DateTime.Now.Date).Count();
            if (numberOfAppointments != 1)
            {
                return $"{numberOfAppointments} afspraken";
            }
            return $"{numberOfAppointments} afspraak";
        }
    }
}
