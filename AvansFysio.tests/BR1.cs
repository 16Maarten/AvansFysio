using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using DomainServices;
using AvansFysio.Controllers;
using Domain;
using AvansFysio.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AvansFysio.tests
{
    public class AppointmentLimitTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void MaxAmountOfAppointmentsExceeded(int days)
        {
            // Arrange
            var patientRepositoryMock = new Mock<IPatientRepository>();
            var patientFileRepositoryMock = new Mock<IPatientFileRepository>();
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var physiotherapistRepositoryMock = new Mock<IPhysiotherapistRepository>();
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();

            var sut = new AppointmentController(null,physiotherapistRepositoryMock.Object, studentRepositoryMock.Object, appointmentRepositoryMock.Object, patientRepositoryMock.Object, patientFileRepositoryMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "test@gmail.com"),

            }, "mock"));
            sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

            var appointment = new AddAppointmentViewModel()
            {
                Date = new DateTime(2023,1,23,10,0,0).AddDays(days),
                PatientId = 1111
            };
            Patient patient = new Patient
            {
                Name = "Henk",
                Birthday = DateTime.Today.AddYears(-24),
                Gender = "man",
                Email = "henk@gmail.com",
                PhoneNumber = "065873532",
                PatientNumber = 1111,
                Img = "img"
            };
            patientRepositoryMock.Setup(patientRepository => patientRepository.GetAllPatients()).Returns(new[]{ patient });
            patientFileRepositoryMock.Setup(patientFileRepository => patientFileRepository.GetAllPatientFiles()).Returns(
                new[]{
                new PatientFile() {
                Id = 1,
                Patient = patient,
                TreatmentPlan = new TreatmentPlan() { NumberOfTreatmentsPerWeek = 2, DurationTreatment = 30 },
            }});
            Physiotherapist physio = new Physiotherapist(1, "test", "test@gmail.com", "test", 124142141, 12413131)
            {
                Presence = new Presence(1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0))
            };
        physiotherapistRepositoryMock.Setup(physiotherapistRepository => physiotherapistRepository.GetAllPhysiotherapists()).Returns(new[] { physio });
            
        appointmentRepositoryMock.Setup(appointmentRepository => appointmentRepository.GetAllAppointments()).Returns(new[]{ new Appointment { Date = new DateTime(2023, 1, 27, 11, 0, 0), Patient = patient, Physiotherapist = physio }, new Appointment { Date = new DateTime(2023, 1, 26, 11, 0, 0), Patient = patient, Physiotherapist = physio } });

            // Act
            var result = sut.AppointmentForm(appointment) as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("Date"));
            Assert.Equal("Je kunt niet meer afspraken inplannen deze week voor deze patient!", result.ViewData.ModelState["Date"].Errors.First().ErrorMessage);
            Assert.Null(result.ViewName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void MaxAmountOfAppointmentsNotExceeded(int days)
        {
            // Arrange
            var patientRepositoryMock = new Mock<IPatientRepository>();
            var patientFileRepositoryMock = new Mock<IPatientFileRepository>();
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var physiotherapistRepositoryMock = new Mock<IPhysiotherapistRepository>();
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();

            var sut = new AppointmentController(null, physiotherapistRepositoryMock.Object, studentRepositoryMock.Object, appointmentRepositoryMock.Object, patientRepositoryMock.Object, patientFileRepositoryMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "test@gmail.com"),

            }, "mock"));
            sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

            var appointment = new AddAppointmentViewModel()
            {
                Date = new DateTime(2023, 1, 23, 10, 0, 0).AddDays(days),
                PatientId = 1111
            };
            Patient patient = new Patient
            {
                Name = "Henk",
                Birthday = DateTime.Today.AddYears(-24),
                Gender = "man",
                Email = "henk@gmail.com",
                PhoneNumber = "065873532",
                PatientNumber = 1111,
                Img = "img"
            };
            patientRepositoryMock.Setup(patientRepository => patientRepository.GetAllPatients()).Returns(new[] { patient });
            patientFileRepositoryMock.Setup(patientFileRepository => patientFileRepository.GetAllPatientFiles()).Returns(
                new[]{
                new PatientFile() {
                Id = 1,
                Patient = patient,
                TreatmentPlan = new TreatmentPlan() { NumberOfTreatmentsPerWeek = 2, DurationTreatment = 30 },
            }});
            Physiotherapist physio = new Physiotherapist(1, "test", "test@gmail.com", "test", 124142141, 12413131)
            {
                Presence = new Presence(1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0))
            };
            physiotherapistRepositoryMock.Setup(physiotherapistRepository => physiotherapistRepository.GetAllPhysiotherapists()).Returns(new[] { physio });

            appointmentRepositoryMock.Setup(appointmentRepository => appointmentRepository.GetAllAppointments()).Returns(new[] { new Appointment { Date = new DateTime(2023, 1, 17, 11, 0, 0), Patient = patient, Physiotherapist = physio }, new Appointment { Date = new DateTime(2023, 1, 16, 11, 0, 0), Patient = patient, Physiotherapist = physio } });

            // Act
            var result = sut.AppointmentForm(appointment);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Appointment", redirectToActionResult.ActionName);
        }
    }
}
