using AvansFysio.Models;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IPhysiotherapistRepository PhysiotherapistRepository, IStudentRepository studentRepository, IPatientRepository patientRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _physiotherapistRepository = PhysiotherapistRepository;
            _patientRepository = patientRepository;
            _studentRepository = studentRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password,false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
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
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    int employee = _physiotherapistRepository.GetAllPhysiotherapists().Where(p => p.Email.Equals(user.Email)).Count();
                    employee = + _studentRepository.GetAllStudents().Where(p => p.Email.Equals(user.Email)).Count();
                    if (employee == 1)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Employee", "true"));
                    }
                    int patient = _patientRepository.GetAllPatients().Where(p => p.Email.Equals(user.Email)).Count();
                    if (patient == 1)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Patient", "true"));
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

    }
}
