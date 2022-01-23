using AvansFysio.Controllers;
using AvansFysio.Models;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AvansFysio.tests
{
    public class BR4
    {
        [Fact]
        public void TreatmentRequiredSpecifics()
        {
            // Arrange
            var patientRepositoryMock = new Mock<IPatientRepository>();
            var patientFileRepositoryMock = new Mock<IPatientFileRepository>();
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var physiotherapistRepositoryMock = new Mock<IPhysiotherapistRepository>();
            var treatmentRepositoryMock = new Mock<ITreatmentRepository>();
            var vektisRepositoryMock = new Mock<IVektisRepository>();
            var sut = new TreatmentController(patientRepositoryMock.Object, physiotherapistRepositoryMock.Object, studentRepositoryMock.Object, patientFileRepositoryMock.Object, treatmentRepositoryMock.Object, vektisRepositoryMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "test@gmail.com"),

            }, "mock"));
            sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

            var treatment = new AddTreatmentViewModel()
            {
                Type = "1751",
                TreatmentDate = DateTime.Now,
                Room = "oefenzaal"
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
            vektisRepositoryMock.Setup(vektisRepository => vektisRepository.GetTreatmentByCode(It.IsAny<string>())).Returns(new VektisTreatment { Code = "1751", Description = "hoofd", RemarkRequired = true });

            // Act
            var result = sut.TreatmentForm(1, treatment) as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(result.ViewData.ModelState.ContainsKey("Specifics"));
            Assert.Equal("Bij dit type behandeling moeten de bijzonderheden ingevuld worden!", result.ViewData.ModelState["Specifics"].Errors.First().ErrorMessage);
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void TreatmentAddWorking()
        {
            // Arrange
            var patientRepositoryMock = new Mock<IPatientRepository>();
            var patientFileRepositoryMock = new Mock<IPatientFileRepository>();
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var physiotherapistRepositoryMock = new Mock<IPhysiotherapistRepository>();
            var treatmentRepositoryMock = new Mock<ITreatmentRepository>();
            var vektisRepositoryMock = new Mock<IVektisRepository>();
            var sut = new TreatmentController(patientRepositoryMock.Object, physiotherapistRepositoryMock.Object, studentRepositoryMock.Object, patientFileRepositoryMock.Object, treatmentRepositoryMock.Object, vektisRepositoryMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, "test@gmail.com"),

            }, "mock"));
            sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

            var treatment = new AddTreatmentViewModel()
            {
                PatientFileId = 1,
                Type = "1751",
                TreatmentDate = DateTime.Now,
                PersonEmail = "test@gmail.com",
                Room = "oefenzaal"
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
            List<Treatment> treatments = new List<Treatment>();
            treatments.Add(new Treatment());
            patientRepositoryMock.Setup(patientRepository => patientRepository.GetAllPatients()).Returns(new[] { patient });
            patientFileRepositoryMock.Setup(patientFileRepository => patientFileRepository.GetWhereIdPatientFile(It.IsAny<int>())).Returns(
                    new PatientFile
                    {
                        Id = 1,
                        Patient = patient,
                        TreatmentPlan = new TreatmentPlan() { NumberOfTreatmentsPerWeek = 2, DurationTreatment = 30 },
                        Treatments = treatments as ICollection<Treatment>
                    });

            Physiotherapist physio = new Physiotherapist(1, "test", "test@gmail.com", "test", 124142141, 12413131)
            {
                Presence = new Presence(1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0))
            };
            physiotherapistRepositoryMock.Setup(physiotherapistRepository => physiotherapistRepository.GetAllPhysiotherapists()).Returns(new[] { physio });
            vektisRepositoryMock.Setup(vektisRepository => vektisRepository.GetTreatmentByCode(It.IsAny<string>())).Returns(new VektisTreatment { Code = "1751", Description = "hoofd", RemarkRequired = false });

            // Act
            var result = sut.TreatmentForm(1, treatment);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
