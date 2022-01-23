using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class AppointmentController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientFileRepository _patientFileRepository;


        public AppointmentController(SignInManager<IdentityUser> signInManager, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IPatientFileRepository patientFileRepository)
        {
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _appointmentRepository = appointmentRepository;
            _signInManager = signInManager;
            _patientRepository = patientRepository;
            _patientFileRepository = patientFileRepository;
        }

        public IActionResult Appointment()
        {
            return View(GetAppointments());
        }

        [HttpGet]
        public IActionResult AppointmentForm()
        {
            PrefillSelectOptions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AppointmentForm(AddAppointmentViewModel appointment)
        {
            string personEmail = User.Identity.Name;
            Physiotherapist physiotherapist = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(personEmail)).FirstOrDefault();
            Student student = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(personEmail)).FirstOrDefault();
            if (appointment.PatientId != -1)
            {
                PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient.PatientNumber == appointment.PatientId).FirstOrDefault();
                TimeSpan startAppointment = new TimeSpan(appointment.Date.Hour, appointment.Date.Minute, 0);
                DateTime endAppointmentDate = appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment);
                TimeSpan endAppointment = new TimeSpan(endAppointmentDate.Hour, endAppointmentDate.Minute, 0);
                int amountOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => p.Patient.PatientNumber == appointment.PatientId && p.Date >= appointment.Date && p.Date < appointment.Date.AddDays(7)).Count();
                int amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.PersonEmail == personEmail && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                if (appointment.Date < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt geen afspraak in het verleden plannen!");
                }
                if (appointment.Date.DayOfWeek.ToString().Equals("Saturday") || appointment.Date.DayOfWeek.ToString().Equals("Sunday"))
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt geen afspraak inplannen in het weekend!");
                }
                if (physiotherapist != null)
                {
                    if (appointment.Date.DayOfWeek.ToString().Equals("Monday") && (startAppointment < physiotherapist.Presence.StartMonday || endAppointment > physiotherapist.Presence.EndMonday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Maandag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Tuesday") && (startAppointment < physiotherapist.Presence.StartTuesday || endAppointment > physiotherapist.Presence.EndTuesday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Dinsdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Wednesday") && (startAppointment < physiotherapist.Presence.StartWednesday || endAppointment > physiotherapist.Presence.EndWednesday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Woensdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Thursday") && (startAppointment < physiotherapist.Presence.StartThursday || endAppointment > physiotherapist.Presence.EndThursday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Donderdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Friday") && (startAppointment < physiotherapist.Presence.StartFriday || endAppointment > physiotherapist.Presence.EndFriday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Vrijdag!");
                    }
                }
                else
                {
                    if (appointment.Date.DayOfWeek.ToString().Equals("Monday") && startAppointment < student.Presence.StartMonday && endAppointment > student.Presence.EndMonday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Maandag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Tuesday") && startAppointment < student.Presence.StartTuesday && endAppointment > student.Presence.EndTuesday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Dinsdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Wednesday") && startAppointment < student.Presence.StartWednesday && endAppointment > student.Presence.EndWednesday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Woensdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Thursday") && startAppointment < student.Presence.StartThursday && endAppointment > student.Presence.EndThursday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Donderdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Friday") && startAppointment < student.Presence.StartFriday && endAppointment > student.Presence.EndFriday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Vrijdag!");
                    }
                }
                if (amountOfAppointments +1 > patientfile.TreatmentPlan.NumberOfTreatmentsPerWeek)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt niet meer afspraken inplannen deze week voor deze patient!");
                }
                if (amountofAppointmentsPhysiotherapist > 0) {
                    ModelState.AddModelError(nameof(appointment.Date), "Er staat al een afspraak gepland op dit moment!");
                }
            }
            else {
                ModelState.AddModelError(nameof(appointment.PatientId), "Er moet een patient ingevoerd worden!");
            }
            if (ModelState.IsValid)
            {
                Patient patient = _patientRepository.GetAllPatients().Where(p => p.PatientNumber == appointment.PatientId).First();
                PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient.PatientNumber == appointment.PatientId).FirstOrDefault();
                var newAppointment = new Appointment
                {
                    Date = appointment.Date,
                    PersonEmail = personEmail,
                    Patient = patient,
                    SessionLength = patientfile.TreatmentPlan.DurationTreatment
                };

                _appointmentRepository.AddAppointment(newAppointment);
                return RedirectToAction("Appointment");
            }
            else
            {
                PrefillSelectOptions();
                return View(appointment);
            }
        }

        [HttpGet]
        public IActionResult AppointmentFormUpdate(int id)
        {
            PrefillSelectOptions();
            Appointment appointment = _appointmentRepository.GetWhereIdAppointment(id);
            var originalAppointment = new AddAppointmentViewModel
            {
                Date = appointment.Date,
                PatientId = appointment.Patient.PatientNumber
            };
            return View(originalAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentFormUpdate(int id,AddAppointmentViewModel appointment)
        {
            string personEmail = _signInManager.Context.User.Identity.Name;
            Physiotherapist physiotherapist = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(personEmail)).FirstOrDefault();
            Student student = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(personEmail)).FirstOrDefault();
            if (appointment.PatientId != -1)
            {
                PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient.PatientNumber == appointment.PatientId).FirstOrDefault();
                TimeSpan startAppointment = new TimeSpan(appointment.Date.Hour, appointment.Date.Minute, 0);
                DateTime endAppointmentDate = appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment);
                TimeSpan endAppointment = new TimeSpan(endAppointmentDate.Hour, endAppointmentDate.Minute, 0);
                int amountOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => p.Patient.PatientNumber == appointment.PatientId && p.Date >= appointment.Date && p.Date < appointment.Date.AddDays(7)).Count();
                int amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.PersonEmail == personEmail && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                if (appointment.Date < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt geen afspraak in het verleden plannen!");
                }
                if (appointment.Date.DayOfWeek.ToString().Equals("Saturday") || appointment.Date.DayOfWeek.ToString().Equals("Sunday"))
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt geen afspraak inplannen in het weekend!");
                }
                if (student == null)
                {
                    if (appointment.Date.DayOfWeek.ToString().Equals("Monday") && (startAppointment < physiotherapist.Presence.StartMonday || endAppointment > physiotherapist.Presence.EndMonday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Maandag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Tuesday") && (startAppointment < physiotherapist.Presence.StartTuesday || endAppointment > physiotherapist.Presence.EndTuesday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Dinsdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Wednesday") && (startAppointment < physiotherapist.Presence.StartWednesday || endAppointment > physiotherapist.Presence.EndWednesday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Woensdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Thursday") && (startAppointment < physiotherapist.Presence.StartThursday || endAppointment > physiotherapist.Presence.EndThursday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Donderdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Friday") && (startAppointment < physiotherapist.Presence.StartFriday || endAppointment > physiotherapist.Presence.EndFriday))
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Vrijdag!");
                    }
                }
                else
                {
                    if (appointment.Date.DayOfWeek.ToString().Equals("Monday") && startAppointment < student.Presence.StartMonday && endAppointment > student.Presence.EndMonday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Maandag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Tuesday") && startAppointment < student.Presence.StartTuesday && endAppointment > student.Presence.EndTuesday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Dinsdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Wednesday") && startAppointment < student.Presence.StartWednesday && endAppointment > student.Presence.EndWednesday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Woensdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Thursday") && startAppointment < student.Presence.StartThursday && endAppointment > student.Presence.EndThursday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Donderdag!");
                    }
                    if (appointment.Date.DayOfWeek.ToString().Equals("Friday") && startAppointment < student.Presence.StartFriday && endAppointment > student.Presence.EndFriday)
                    {
                        ModelState.AddModelError(nameof(appointment.Date), "Deze tijden worden er niet gewerkt op Vrijdag!");
                    }
                }
                if (amountOfAppointments + 1 > patientfile.TreatmentPlan.NumberOfTreatmentsPerWeek)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt niet meer afspraken inplannen deze week voor deze patient!");
                }
                if (amountofAppointmentsPhysiotherapist > 0)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Er staat al een afspraak gepland op dit moment!");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(appointment.PatientId), "Er moet een patient ingevoerd worden!");
            }
            if (ModelState.IsValid)
            {
                Patient patient = _patientRepository.GetAllPatients().Where(p => p.PatientNumber == appointment.PatientId).First();
                PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient.PatientNumber == appointment.PatientId).FirstOrDefault();
                Appointment originalAppointment = _appointmentRepository.GetWhereIdAppointment(id);
                originalAppointment.Date = appointment.Date;
                originalAppointment.Patient = patient;

                await _appointmentRepository.UpdateAppointment(originalAppointment);
                return RedirectToAction("Appointment");
            }
            else
            {
                PrefillSelectOptions();
                return View(appointment);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentRepository.RemoveAppointment(_appointmentRepository.GetWhereIdAppointment(id));
            return RedirectToAction("Appointment");
        }

        private void PrefillSelectOptions()
        {
            var patients = _patientRepository.GetAllPatients().Prepend(new Patient() { PatientNumber = -1, Name = "Select a patient" });
            ViewBag.Patients = new SelectList(patients, "PatientNumber", "Name");
        }

        private IEnumerable<Appointment> GetAppointments()
        {
            string personEmail = _signInManager.Context.User.Identity.Name;
            IEnumerable<Appointment> Appointments = _appointmentRepository.GetAllAppointments().Where(p => p.PersonEmail.Equals(personEmail) && p.Date >= DateTime.Now && p.Date <= DateTime.Now.AddDays(7)).OrderBy(p => p.Date);
            return Appointments;
        }
    }
}
