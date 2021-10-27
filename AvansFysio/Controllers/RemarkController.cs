using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    public class RemarkController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IRemarkRepository _remarkRepository;


        public RemarkController(IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IPatientFileRepository patientFileRepository, IRemarkRepository remarkRepository)
        {
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _patientFileRepository = patientFileRepository;
            _remarkRepository = remarkRepository;
        }
        public IActionResult Treatment(int id)
        {
            return View(_remarkRepository.GetWhereIdRemark(id));
        }


        [HttpGet]
        /*public IActionResult RemarkForm(int id)
        {
            var model = new AddRemarkViewModel();
            PrefillSelectOptions();
            model.PatientFileId = id;
            return View(model);
        }
        */
        private void PrefillSelectOptions()
        {
            var students = _studentRepository.GetAllStudents();
            var physiotherapists = _physiotherapistRepository.GetAllPhysiotherapists();
            var persons = new List<IPerson>();
            foreach (var student in students)
            {
                persons.Add(student);
            }
            foreach (var physiotherapist in physiotherapists)
            {
                persons.Add(physiotherapist);
            }
            ViewBag.Persons = new SelectList(persons, "Email", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TreatmentForm(int id, AddTreatmentViewModel treatment)
        {
            if (ModelState.IsValid)
            {
                var result = new Treatment
                {
                    Type = treatment.Type,
                    Room = treatment.Room,
                    Specifics = treatment.Specifics,
                    Description = treatment.Description,
                    TreatmentDate = treatment.TreatmentDate
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
                return RedirectToAction("Index");
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
