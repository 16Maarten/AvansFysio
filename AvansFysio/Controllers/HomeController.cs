﻿using AvansFysio.Models;
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
        private readonly IPatientRepository _patientRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPhysiotherapistRepository _physiotherapistRepository;


        public HomeController(IPatientRepository patientRepository, IStudentRepository studentRepository, IPhysiotherapistRepository physiotherapistRepository)
        {
            _patientRepository = patientRepository;
            _studentRepository = studentRepository;
            _physiotherapistRepository = physiotherapistRepository;
        }

        public IActionResult Index()
        {
            return View(_patientRepository.GetAllPatients());
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
