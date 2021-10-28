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
    public class PatientAccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientFileRepository _patientFileRepository;


        public PatientAccountController(SignInManager<IdentityUser> signInManager,IPatientRepository patientRepository, IPatientFileRepository patientFileRepository)
        {
            _signInManager = signInManager;
            _patientRepository = patientRepository;
            _patientFileRepository = patientFileRepository;
        }
        [Authorize(Policy = "PatientOnly")]
        public IActionResult Patient()
        {
            return View(GetPatient().ToViewModel());
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
                return View(patientFile.ToViewModel());
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
