using AvansFysio.Models;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IClaimRepository _claimRepository;

        public AccountController(IClaimRepository claimRepository, UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IPhysiotherapistRepository PhysiotherapistRepository, IStudentRepository studentRepository, IPatientRepository patientRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _physiotherapistRepository = PhysiotherapistRepository;
            _patientRepository = patientRepository;
            _studentRepository = studentRepository;
            _claimRepository = claimRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        string claim = this._claimRepository.GetClaim(user.UserName);
                        if (claim.Equals("Employee"))
                        {
                            return RedirectToAction("Index", "Patient");
                        }
                        if (claim.Equals("Patient"))
                        {
                            return RedirectToAction("Patient", "PatientAccount");
                        } 
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "Verkeerde email of wachtwoord");
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var EmailsStudents = _studentRepository.GetAllStudents().Any(p => p.Email == model.Email);
            var EmailsPatients = _patientRepository.GetAllPatients().Any(p => p.Email == model.Email);
            var EmailsPhysiotherapists = _physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email == model.Email);
            if (!(EmailsStudents || EmailsPatients || EmailsPhysiotherapists))
            {
                ModelState.AddModelError(nameof(model.Email),
                    "Email is niet bekend in het systeem!");
            }
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };


                if ((await _userManager.CreateAsync(user, model.Password)).Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (_physiotherapistRepository.GetAllPhysiotherapists().Any(p => p.Email.Equals(user.Email)) || _studentRepository.GetAllStudents().Any(p => p.Email.Equals(user.Email)))
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Employee", "true"));
                    }
                    if (_patientRepository.GetAllPatients().Any(p => p.Email.Equals(user.Email)))
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Patient", "true"));
                    }
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "Registreren mislukt probeer opnieuw");

            }
            return View(model);
        }

    }
}
