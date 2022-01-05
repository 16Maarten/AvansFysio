using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class TreatmentController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IVektisRepository _vektisRepository;


        public TreatmentController(IPatientRepository patientRepository, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IPatientFileRepository patientFileRepository, ITreatmentRepository treatmentRepository, IVektisRepository vektisRepository)
        {
            _patientRepository = patientRepository;
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _patientFileRepository = patientFileRepository;
            _treatmentRepository = treatmentRepository;
            _vektisRepository = vektisRepository;
        }
        public IActionResult Treatment(int id)
        {
            return View(_treatmentRepository.GetWhereIdTreatment(id).ToViewModel());
        }

        [HttpGet]
        public IActionResult TreatmentFormUpdate(int id)
        {
            var model = new AddTreatmentViewModel();
            PrefillSelectOptions();
            model.PatientFileId = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TreatmentFormUpdate(int id, AddTreatmentViewModel treatment)
        {
            if (treatment.Type.Equals("Kies een behandeling"))
            {
                ModelState.AddModelError(nameof(treatment.Type), "Er moet een type behandeling gekozen worden");
            }
            else
            {
                if (_vektisRepository.GetTreatmentByCode(treatment.Type).RemarkRequired)
                {
                    ModelState.AddModelError(nameof(treatment.Specifics), "Bij dit type behandeling moeten de bijzonderheden ingevuld worden!");
                }
            }
            if (ModelState.IsValid)
            {
                var updateTreatment = _treatmentRepository.GetWhereIdTreatment(id);
                updateTreatment.Type = treatment.Type;
                updateTreatment.Room = treatment.Room;
                updateTreatment.Specifics = treatment.Specifics;
                updateTreatment.Description = _vektisRepository.GetTreatmentByCode(treatment.Type).Description;
                updateTreatment.TreatmentDate = treatment.TreatmentDate;
                if (treatment.PersonEmail != null)
                {
                    if (_physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email.Equals(treatment.PersonEmail)))
                    {
                        updateTreatment.Physiotherapist = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(treatment.PersonEmail)).First();
                    }
                    else
                    {
                        updateTreatment.Student = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(treatment.PersonEmail)).First();
                    }
                }

                await _treatmentRepository.UpdateTreatment(updateTreatment);
                return RedirectToAction("Index", "PatientFile");
            }
            else
            {
                PrefillSelectOptions();
                return View(treatment);
            }
        }


        [HttpGet]
        public IActionResult TreatmentForm(int id)
        {
            var model = new AddTreatmentViewModel();
            PrefillSelectOptions();
            model.PatientFileId = id;
            return View(model);
        }

        private void PrefillSelectOptions()
        {
            var students = _studentRepository.GetAllStudents();
            var physiotherapists = _physiotherapistRepository.GetAllPhysiotherapists();
            var persons = new List<IPerson>();
            foreach (var student in students) {
                persons.Add(student);
            }
            foreach (var physiotherapist in physiotherapists)
            {
                persons.Add(physiotherapist);
            }
            ViewBag.Persons = new SelectList(persons, "Email", "Name");

            var treatments = _vektisRepository.GetAllTreatments().Prepend(new VektisTreatment { Code = "Kies een behandeling" });
            ViewBag.Treatments = new SelectList(treatments, "Code", "Code");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _treatmentRepository.RemoveTreatment(_treatmentRepository.GetWhereIdTreatment(id));
            return RedirectToAction("Index", "PatientFile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TreatmentForm(int id,AddTreatmentViewModel treatment)
        {
            if (treatment.Type.Equals("Kies een behandeling"))
            {
                ModelState.AddModelError(nameof(treatment.Type), "Er moet een type behandeling gekozen worden");
            }
            else {
                if (_vektisRepository.GetTreatmentByCode(treatment.Type).RemarkRequired) {
                    ModelState.AddModelError(nameof(treatment.Specifics), "Bij dit type behandeling moeten de bijzonderheden ingevuld worden!");
                }
            }
            if (ModelState.IsValid) {
            var result = new Treatment
            {
                Type = treatment.Type,
                Room = treatment.Room,
                Specifics = treatment.Specifics,
                Description = _vektisRepository.GetTreatmentByCode(treatment.Type).Description,
                TreatmentDate = treatment.TreatmentDate,
                CreationDate = DateTime.Now
            };
            if (treatment.PersonEmail != null)
            {
                if (_physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email.Equals(treatment.PersonEmail)))
                {
                    result.Physiotherapist = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(treatment.PersonEmail)).First();
                }
                else
                {
                    result.Student = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(treatment.PersonEmail)).First();
                }
            }
                var patientFile = _patientFileRepository.GetWhereIdPatientFile(id);
                patientFile.Treatments.Add(result);
                await _patientFileRepository.UpdatePatientFile(patientFile);
            return RedirectToAction("Index","PatientFile");
            }
            else
            {
                PrefillSelectOptions();
                return View(treatment);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
