using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Controllers
{
    [Authorize(Policy = "EmployeeOnly")]
    public class PresenceController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;
        private readonly IPresenceRepository _presenceRepository;


        public PresenceController(SignInManager<IdentityUser> signInManager, IPhysiotherapistRepository physiotherapistRepository, IStudentRepository studentRepository, IPresenceRepository presenceRepository)
        {
            _physiotherapistRepository = physiotherapistRepository;
            _studentRepository = studentRepository;
            _presenceRepository = presenceRepository;
            _signInManager = signInManager;
        }

        public IActionResult Presence()
        {
            return View(GetPresence());
        }

        [HttpGet]
        public IActionResult PresenceForm()
        {
            Presence presence = GetPresence();
            UpdatePresenceViewModel updatePresenceViewModel = new UpdatePresenceViewModel
            {
                StartMonday = presence.StartMonday,
                EndMonday = presence.EndMonday,
                StartTuesday = presence.StartTuesday,
                EndTuesday = presence.EndTuesday,
                StartWednesday = presence.StartWednesday,
                EndWednesday = presence.EndWednesday,
                StartThursday = presence.StartThursday,
                EndThursday = presence.EndThursday,
                StartFriday = presence.StartFriday,
                EndFriday = presence.EndFriday,
            };
            return View(updatePresenceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PresenceForm(UpdatePresenceViewModel presence)
        {
            if (presence.StartMonday > presence.EndMonday)
            {
                ModelState.AddModelError(nameof(presence.StartMonday),"Deze tijden zijn niet mogelijk");
                ModelState.AddModelError(nameof(presence.EndMonday), "Deze tijden zijn niet mogelijk");
            }
            if (presence.StartTuesday > presence.EndTuesday)
            {
                ModelState.AddModelError(nameof(presence.StartTuesday), "Deze tijden zijn niet mogelijk");
                ModelState.AddModelError(nameof(presence.EndTuesday), "Deze tijden zijn niet mogelijk");
            }
            if (presence.StartWednesday > presence.EndWednesday)
            {
                ModelState.AddModelError(nameof(presence.StartWednesday), "Deze tijden zijn niet mogelijk");
                ModelState.AddModelError(nameof(presence.EndWednesday), "Deze tijden zijn niet mogelijk");
            }
            if (presence.StartThursday > presence.EndThursday)
            {
                ModelState.AddModelError(nameof(presence.StartThursday), "Deze tijden zijn niet mogelijk");
                ModelState.AddModelError(nameof(presence.EndThursday), "Deze tijden zijn niet mogelijk");
            }
            if (presence.StartFriday > presence.EndFriday)
            {
                ModelState.AddModelError(nameof(presence.StartFriday), "Deze tijden zijn niet mogelijk");
                ModelState.AddModelError(nameof(presence.EndFriday), "Deze tijden zijn niet mogelijk");
            }
            if (ModelState.IsValid)
            {
                var updatePresence = GetPresence();
                updatePresence.StartMonday = presence.StartMonday;
                updatePresence.EndMonday = presence.EndMonday;
                updatePresence.StartTuesday = presence.StartTuesday;
                updatePresence.EndTuesday = presence.EndTuesday;
                updatePresence.StartWednesday = presence.StartWednesday;
                updatePresence.EndWednesday = presence.EndWednesday;
                updatePresence.StartThursday = presence.StartThursday;
                updatePresence.EndThursday = presence.EndThursday;
                updatePresence.StartFriday = presence.StartFriday;
                await _presenceRepository.UpdatePresence(updatePresence);
                return RedirectToAction("Presence");
            }
            else
            {
                return View(presence);
            }
        }

        private Presence GetPresence() { 
            string personEmail = _signInManager.Context.User.Identity.Name;
            Presence presence = null;
            Physiotherapist physio = _physiotherapistRepository.GetWhereEmailPhysiotherapist(personEmail);
            if (physio != null) {
                presence = physio.Presence;
            }else{
                presence = _studentRepository.GetAllStudents().Where(p => p.Email.Equals(personEmail)).First().Presence;
            }
            return presence;
        }
    }
}
