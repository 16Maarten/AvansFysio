using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IRemarkRepository _remarkRepository;


        public RemarkController(SignInManager<IdentityUser> signInManager, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IPatientFileRepository patientFileRepository, IRemarkRepository remarkRepository)
        {
            _signInManager = signInManager;
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _patientFileRepository = patientFileRepository;
            _remarkRepository = remarkRepository;
        }
        [Authorize]
        public IActionResult Remark(int id)
        {
            return View(_remarkRepository.GetWhereIdRemark(id));
        }

        [Authorize(Policy = "EmployeeOnly")]
        [HttpGet]
        public IActionResult RemarkForm(int id)
        {
            var model = new AddRemarkViewModel();
            model.PatientFileId = id;
            return View(model);
        }
        [Authorize(Policy = "EmployeeOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemarkForm(int id, AddRemarkViewModel remark)
        {
            if (ModelState.IsValid)
            {
                var result = new Remark
                {
                    Description = remark.Description,
                    Visible = remark.Visibility,
                    RemarkDate = DateTime.Now,
                };
                String personEmail = _signInManager.Context.User.Identity.Name;
                if (_physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email.Equals(personEmail)))
                {
                    result.Physiotherapist = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(personEmail)).First();
                }
                else
                {
                    result.Student = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(personEmail)).First();
                }

                var patientFile = _patientFileRepository.GetWhereIdPatientFile(id);
                patientFile.Remarks.Add(result);
                await _patientFileRepository.UpdatePatientFile(patientFile);
                return RedirectToAction("Index", "PatientFile");
            }
            else
            {
                return View(remark);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
