using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class PatientFileController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IVektisRepository _vektisRepository;


        public PatientFileController(IPatientRepository patientRepository, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IPatientFileRepository patientFileRepository, IVektisRepository vektisRepository)
        {
            _patientRepository = patientRepository;
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _patientFileRepository = patientFileRepository;
            _vektisRepository = vektisRepository;
        }

        [HttpGet]
        public IActionResult PatientFileForm()
        {
            var model = new AddPatientFileViewModel();

            PrefillSelectOptions();
            return View(model);
        }

        public IActionResult PatientFile(int id)
        {
                return View(_patientFileRepository.GetWhereIdPatientFile(id).ToViewModel());
        }

        public IActionResult Index()
        {
            return View(_patientFileRepository.GetAllPatientFiles().ToViewModel());
        }

        private void PrefillSelectOptions()
        {
            var patients = _patientRepository.GetAllPatients().Prepend(new Patient() { PatientNumber = -1, Name = "Select a patient" });
            ViewBag.Patients = new SelectList(patients, "PatientNumber", "Name");

            var students = _studentRepository.GetAllStudents().Prepend(new Student { Id = -1, Name = "Select a student" });
            ViewBag.Students = new SelectList(students, "Id", "Name");

            var physiotherapists = _physiotherapistRepository.GetAllPhysiotherapists().Prepend(new Physiotherapist { Id = -1, Name = "Select a physiotherapist" });
            ViewBag.Physiotherapists = new SelectList(physiotherapists, "Id", "Name");

            var diagnoses = _vektisRepository.GetAllDiagnoses().Prepend(new VektisDiagnosis {Code = -1});
            ViewBag.Diagnoses = new SelectList(diagnoses, "Code", "Code");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientFileForm(AddPatientFileViewModel patientFile)
        {
            if (patientFile.NumberOfTreatmentsPerWeek < 1)
            {
                ModelState.AddModelError(nameof(patientFile.NumberOfTreatmentsPerWeek),
                    "Er moet minimaal 1 behandeling per week worden gegeven!");
            }
            if (patientFile.DurationTreatment < 1)
            {
                ModelState.AddModelError(nameof(patientFile.DurationTreatment),
                    "Een behandeling kan geen 0 minuten duren!");
            }
            if (patientFile.PatientId == -1)
            {
                ModelState.AddModelError(nameof(patientFile.PatientId),
                    "Er moet een patient opgegeven worden!");
            }
            if (patientFile.StudentId != -1 && patientFile.PhysiotherapistId == -1 || patientFile.PhysiotherapistId == -1)
            {
                ModelState.AddModelError(nameof(patientFile.PhysiotherapistId),
                    "Er moet een fysiotherapeut opgegeven worden!");
            }
            if (patientFile.DiagnosticCode == -1)
            {
                ModelState.AddModelError(nameof(patientFile.DiagnosticCode),
                    "Er moet een DHCP Code opgegeven worden!");
            }

            if (ModelState.IsValid)
            {
                string DiagnoseDescription ="";
                if (patientFile.PatientId != -1)
                {
                    var selectedPatient = _patientRepository.GetWhereIdPatient(patientFile.PatientId);
                    patientFile.Patient = selectedPatient;
                }
                if (patientFile.PatientId != -1)
                {
                    var selectedDiagnose = _vektisRepository.GetDiagnosisByCode(patientFile.DiagnosticCode);
                    DiagnoseDescription = selectedDiagnose.BodyLocation+"  "+ selectedDiagnose.Pathology;
                }
                if (patientFile.StudentId != -1)
                {
                    var selectedStudent = _studentRepository.GetWhereIdStudent(patientFile.StudentId);
                    patientFile.Student = selectedStudent;
                }
                if (patientFile.PhysiotherapistId != -1)
                {
                    var selectedPhysiotherapist = _physiotherapistRepository.GetWhereIdPhysiotherapist(patientFile.PhysiotherapistId);
                    patientFile.Physiotherapist = selectedPhysiotherapist;
                }
                var result = new PatientFile
                {
                    Patient = patientFile.Patient,
                    Age = CalculateAge(patientFile.Patient),
                    Description = patientFile.Description,
                    DiagnosticCode = patientFile.DiagnosticCode,
                    DescriptionDiagnosticCode = DiagnoseDescription,
                    IsStudent = patientFile.IsStudent,
                    Physiotherapist = patientFile.Physiotherapist,
                    IntakeDate = patientFile.IntakeDate
                };
                result.TreatmentPlan = new TreatmentPlan
                {
                    NumberOfTreatmentsPerWeek = patientFile.NumberOfTreatmentsPerWeek,
                    DurationTreatment = patientFile.DurationTreatment,
                    DiagnosticCode = patientFile.DiagnosticCode,
                    DescriptionDiagnosticCode = DiagnoseDescription,
                };
                if (patientFile.Student != null)
                {
                    result.Student = patientFile.Student;
                }
                    result.DischargeDate = patientFile.DischargeDate;

                await _patientFileRepository.AddPatientFile(result);
                return RedirectToAction("Index");
            }
            else
            {
                PrefillSelectOptions();
                return View(patientFile);
            }
        }
        private static int CalculateAge(Patient patient)
        {
            int age = DateTime.Now.Year - patient.Birthday.Year;

            if (patient.Birthday > DateTime.Now.AddYears(-age))age--;

            return age;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _patientFileRepository.RemovePatientFile(_patientFileRepository.GetWhereIdPatientFile(id));
            return RedirectToAction("Index", "PatientFile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
