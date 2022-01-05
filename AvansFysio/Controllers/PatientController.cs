using AvansFysio.Models;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using Domain;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IPhysiotherapistRepository _physiotherapistRepository;
    //private readonly IWebHostEnvironment _webHostEnvironment;


    public PatientController( IPatientRepository patientRepository, IStudentRepository studentRepository, IPhysiotherapistRepository physiotherapistRepository)
    {
        _patientRepository = patientRepository;
        _studentRepository = studentRepository;
        _physiotherapistRepository = physiotherapistRepository;
    }

    public IActionResult Index()
    {
        return View(_patientRepository.GetAllPatients().ToViewModel());
    }

    public IActionResult Patient(int id)
    {
            var patient = _patientRepository.GetWhereIdPatient(id).ToViewModel();
            return View(patient);
    }

        [HttpGet]
        public IActionResult PatientFormUpdate(int id)
        {
            var model = new UpdatePatientViewModel();
            model.PatientId = id;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientFormUpdate(int id,UpdatePatientViewModel patient)
        {
            if (ModelState.IsValid)
            {
                var updatePatient = _patientRepository.GetAllPatients().Where(p => p.PatientNumber.Equals(id)).First();
                updatePatient.Name = patient.Name;
                updatePatient.Birthday = patient.Birthday;
                updatePatient.PhoneNumber = patient.PhoneNumber;
                await _patientRepository.UpdatePatient(updatePatient);
                return RedirectToAction("Index");
            }
            else
            {
                return View(patient);
            }
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
            if (patient.Img != null)
            {
                if (patient.Img.Length > 2000000)
                {
                    ModelState.AddModelError(nameof(patient.Img), "Afbeelding is groter dan 2MB");
                    return View(patient);
                }
            }
            if (ModelState.IsValid)
        {
                var createPatient = new Patient
                {
                    Name = patient.Name,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    IdentificationNumber = patient.IdentificationNumber,
                    Birthday = patient.Birthday,
                    Gender = patient.Gender,
                    Img = imgToBase64(patient.Img)
                };
                await _patientRepository.AddPatient(createPatient);
            return RedirectToAction("Index");
        }
        else
        {
            return View(patient);
        }
    }

        public static string imgToBase64(IFormFile? img)
        {
            if (img != null && img.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    var image = Image.Load(img.OpenReadStream());
                    image.SaveAsJpeg(ms);
                    var fileBytes = ms.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }

            return null;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}
