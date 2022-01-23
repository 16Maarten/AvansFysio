using Domain;
using System;
using Xunit;

namespace AvansFysio.tests
{
    public class BR3
    {
        [Theory]
        [InlineData(-6, -2)]
        [InlineData(-6, -3)]
        [InlineData(1, 5)]
        public void ShowAddTreatmentFail(int intakeday, int dischargeday)
        {
            // Arrange
            var file = new PatientFile() { IntakeDate = DateTime.Now.AddDays(intakeday), DischargeDate= DateTime.Now.AddDays(dischargeday)};
            // act
            Boolean showButton = file.IntakeDate <= DateTime.Now && file.DischargeDate >= DateTime.Now;
            // assert
            Assert.False(showButton);
        }

        [Theory]
        [InlineData(-6, 0)]
        [InlineData(-6, 1)]
        [InlineData(-6, 2)]
        public void ShowAddTreatmentPass(int intakeday, int dischargeday)
        {
            // Arrange
            var file = new PatientFile() { IntakeDate = DateTime.Now.AddDays(intakeday).AddMinutes(1), DischargeDate = DateTime.Now.AddDays(dischargeday).AddMinutes(1) };
            // act
            Boolean showButton = file.IntakeDate <= DateTime.Now && file.DischargeDate >= DateTime.Now;
            // assert
            Assert.True(showButton);
        }

    }
}

