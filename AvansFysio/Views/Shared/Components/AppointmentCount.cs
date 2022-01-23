using Domain;
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
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IStudentRepository _studentRepository;

        public AppointmentCount(IAppointmentRepository appointmentRepository, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
        }

        public string Invoke()
        {
            int numberOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => p.PersonEmail == User.Identity.Name && p.Date.Date == DateTime.Now.Date).Count();
            if (numberOfAppointments != 1)
            {
                return $"{numberOfAppointments} afspraken";
            }
            return $"{numberOfAppointments} afspraak";
        }
    }
}
