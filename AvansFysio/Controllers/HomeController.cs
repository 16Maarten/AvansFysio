using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        //private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, IPatientRepository patientRepository, IStudentRepository studentRepository, IPhysiotherapistRepository physiotherapistRepository)
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _studentRepository = studentRepository;
            _physiotherapistRepository = physiotherapistRepository;
        }

        public IActionResult Index()
        {
            return View(_patientRepository.GetAllPatients().ToViewModel());
        }
        [HttpGet]
        public IActionResult FormPatient()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormPatient(AddPatientViewModel patient)
        {
            var EmailsStudents = _studentRepository.GetAllStudents().Any(p => p.Email == patient.Email);
            var EmailsPatients = _patientRepository.GetAllPatients().Any(p => p.Email == patient.Email);
            var EmailsPhysiotherapists = _physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email == patient.Email);
            if (EmailsStudents || EmailsPatients || EmailsPhysiotherapists)
            {
                ModelState.AddModelError(nameof(patient.Email),
                    "Deze email is al in gebruik");
            }
            if (ModelState.IsValid)
            {
               /* string stringFileName = UploadFile(patient);
                var newPatient = new Patient
                {
                    Name = patient.Name,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    IdentificationNumber = patient.IdentificationNumber,
                    Img = stringFileName,
                    Birthday = patient.Birthday,
                    Gender = patient.Gender,
                    Student = patient.Student
                };
               */
                await _patientRepository.AddPatient(patient.ToDomain());
                return RedirectToAction("Index");
            }
            else {
                return View(patient);
            }
            }
        /*private string UploadFile(AddPatientViewModel patient) {
            string filename = null;
            if (patient.Img != null) { 
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath,)
            }
        }
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
