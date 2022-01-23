using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "PatientOnly")]
    public class PatientAccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IStudentRepository _studentFileRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IAppointmentRepository _appointmentRepository;


        public PatientAccountController(SignInManager<IdentityUser> signInManager,IPatientRepository patientRepository, IPatientFileRepository patientFileRepository, IAppointmentRepository appointmentRepository, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentFileRepository)
        {
            _signInManager = signInManager;
            _patientRepository = patientRepository;
            _patientFileRepository = patientFileRepository;
            _appointmentRepository = appointmentRepository;
            _physiotherapistRepository = physiotherapistRepository;
            _studentFileRepository = studentFileRepository;
        }
        public IActionResult Patient()
        {
            return View(GetPatient());
        }

        public IActionResult Appointment()
        {
            IEnumerable<Appointment> appointments = _appointmentRepository.GetAllAppointments().Where(p => p.Patient == GetPatient() && p.Date >= DateTime.Now && p.Date <= DateTime.Now.AddDays(7)).OrderBy(p => p.Date);
            return View(appointments);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentRepository.RemoveAppointment(_appointmentRepository.GetWhereIdAppointment(id));
            return RedirectToAction("Appointment");
        }

        [HttpGet]
        public IActionResult PatientFormUpdate(int id)
        {
            var model = new UpdatePatientAccountViewModel();
            model.PatientId = id;
            return View(model);
        }

        public IActionResult PatientFile()
        {
            if (_patientFileRepository.GetAllPatientFiles().Any(p => p.Patient.Equals(GetPatient())))
            {
                PatientFile patientFile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient.Equals(GetPatient())).First();
                return View(patientFile);
            }
            else {
                return View();
            }
        }

        private Patient GetPatient() {
            string patientEmail = _signInManager.Context.User.Identity.Name;
            return _patientRepository.GetAllPatients().Where(p => p.Email.Equals(patientEmail)).First();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientFormUpdate(int id, UpdatePatientAccountViewModel patient)
        {
            if (ModelState.IsValid)
            {
                var updatePatient = _patientRepository.GetAllPatients().Where(p => p.PatientNumber.Equals(id)).First();
                updatePatient.PhoneNumber = patient.PhoneNumber;
                await _patientRepository.UpdatePatient(updatePatient);
                return RedirectToAction("Patient");
            }
            else
            {
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult AppointmentForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentForm(AddAppointmentViewModel appointment)
        {
            PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient == GetPatient()).FirstOrDefault();
            Physiotherapist physiotherapist = _physiotherapistRepository.GetWhereIdPhysiotherapist(patientfile.Physiotherapist.Id);
            Student student = _studentFileRepository.GetWhereIdStudent(patientfile.Student.Id);
                TimeSpan startAppointment = new TimeSpan(appointment.Date.Hour, appointment.Date.Minute, 0);
                DateTime endAppointmentDate = appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment);
                TimeSpan endAppointment = new TimeSpan(endAppointmentDate.Hour, endAppointmentDate.Minute, 0);
                int amountOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => p.Patient.PatientNumber == GetPatient().IdentificationNumber && p.Date >= appointment.Date && p.Date < appointment.Date.AddDays(7)).Count();
                int amountofAppointmentsPhysiotherapist;
                if (physiotherapist != null)
                {
                    amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.Physiotherapist == physiotherapist && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                }
                else
                {
                    amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.Student == student && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                }
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
                if (amountOfAppointments + 1 > patientfile.TreatmentPlan.NumberOfTreatmentsPerWeek)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt niet meer afspraken inplannen deze week voor deze patient!");
                }
                if (amountofAppointmentsPhysiotherapist > 0)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Er staat al een afspraak gepland op dit moment!");
                }
            if (ModelState.IsValid)
            {
                var newAppointment = new Appointment
                {
                    Date = appointment.Date,
                    Physiotherapist = physiotherapist,
                    Student = student,
                    Patient = GetPatient(),
                    SessionLength = patientfile.TreatmentPlan.DurationTreatment
                };

                await _appointmentRepository.AddAppointment(newAppointment);
                return RedirectToAction("Appointment");
            }
            else
            {
                return View(appointment);
            }
        }

        [HttpGet]
        public IActionResult AppointmentFormUpdate(int id)
        {
            Appointment appointment = _appointmentRepository.GetWhereIdAppointment(id);
            var originalAppointment = new AddAppointmentViewModel
            {
                Date = appointment.Date,
            };
            return View(originalAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentFormUpdate(int id, AddAppointmentViewModel appointment)
        {
            PatientFile patientfile = _patientFileRepository.GetAllPatientFiles().Where(p => p.Patient == GetPatient()).FirstOrDefault();
            Physiotherapist physiotherapist = _physiotherapistRepository.GetWhereIdPhysiotherapist(patientfile.Physiotherapist.Id);
            Student student = _studentFileRepository.GetWhereIdStudent(patientfile.Student.Id);
            TimeSpan startAppointment = new TimeSpan(appointment.Date.Hour, appointment.Date.Minute, 0);
                DateTime endAppointmentDate = appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment);
                TimeSpan endAppointment = new TimeSpan(endAppointmentDate.Hour, endAppointmentDate.Minute, 0);
                int amountOfAppointments = _appointmentRepository.GetAllAppointments().Where(p => p.Patient.PatientNumber == GetPatient().IdentificationNumber && p.Date >= appointment.Date && p.Date < appointment.Date.AddDays(7)).Count();
                int amountofAppointmentsPhysiotherapist;
                if (physiotherapist != null)
                {
                    amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.Physiotherapist == physiotherapist && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                }
                else
                {
                    amountofAppointmentsPhysiotherapist = _appointmentRepository.GetAllAppointments().Where(p => p.Student == student && p.Date >= appointment.Date && p.Date <= appointment.Date.AddMinutes(patientfile.TreatmentPlan.DurationTreatment)).Count();
                }
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
                if (amountOfAppointments + 1 > patientfile.TreatmentPlan.NumberOfTreatmentsPerWeek)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Je kunt niet meer afspraken inplannen deze week voor deze patient!");
                }
                if (amountofAppointmentsPhysiotherapist > 0)
                {
                    ModelState.AddModelError(nameof(appointment.Date), "Er staat al een afspraak gepland op dit moment!");
                }
            if (ModelState.IsValid)
            {
                Appointment originalAppointment = _appointmentRepository.GetWhereIdAppointment(id);
                originalAppointment.Date = appointment.Date;

                await _appointmentRepository.UpdateAppointment(originalAppointment);
                return RedirectToAction("Appointment");
            }
            else
            {
                return View(appointment);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
