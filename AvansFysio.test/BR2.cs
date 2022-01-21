using AvansFysio.Controllers;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

/*namespace AvansFysio.test
{
    public class BR2
    {
        // Het maximaal aantal afspraken per week wordt niet overschreden bij het boeken van een afspraak.
        [Theory]
        [InlineData(0, 2, true)]
        [InlineData(1, 2, true)]
        [InlineData(2, 2, false)]
        public void AddAppointmentMaxAppointmentCheck(int amount, int max, bool shouldPass)
        {
            // Arrange
            //private readonly SignInManager<IdentityUser> _signInManager;
            var _studentRepository = new Mock<IStudentRepository>();
            var _appointmentRepository = new Mock<IAppointmentRepository>();
            var _patientFileRepository = new Mock<IPatientFileRepository>();
            var _patientRepository = new Mock<IPatientRepository>();
            var _physiotherapistRepository = new Mock<IPhysiotherapistRepository>();


            var controller = new AppointmentController(null,_physiotherapistRepository.Object,_studentRepository.Object, _appointmentRepository.Object, _patientRepository.Object, _patientFileRepository.Object);

            Patient patient = new Patient { PatientNr = 1, Name = "Melvin", Email = "Melvin@giebels.nl", PhoneNr = "0612345678", Birthdate = DateTime.Now.AddYears(-(18)), Gender = "Man" };
            appointmentRepo.Setup(a => a.GetAppointmentsPatientById(patient.PatientNr)).Returns(appointmentsPatient)

            TreatmentPlan treatmentPlan = new TreatmentPlan { NrOfTreatments = max, SessionDuration = 30 };

            PatientFile patientFile = new PatientFile { TreatmentPlan = treatmentPlan, Patient = patient };
            DateTime datetime = DateTime.Now.AddDays(2);
            var appointment = new Appointment { Patient = patient, Physiotherapist = physiotherapist, Date = datetime, SessionLength = 30 };




            var result = controller.MaxSessionsPerWeek(appointment.Date, [], patientFile.TreatmentPlan.NrOfTreatments, 0);

            var i = 1 + 1;
        }
}*/
