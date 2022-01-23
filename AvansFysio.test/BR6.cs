using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AvansFysio.tests
{
    public class BR6
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void ShowDeleteButtonFail(int appointmentday)
        {
            // Arrange
            var appointment = new Appointment() { Date = DateTime.Now.AddDays(appointmentday) };
            // act
            Boolean showButton = appointment.Date > DateTime.Now.AddDays(1);
            // assert
            Assert.False(showButton);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShowDeleteButtonPass(int appointmentday)
        {
            // Arrange
            var appointment = new Appointment() { Date = DateTime.Now.AddDays(appointmentday).AddMinutes(1) };
            // act
            Boolean showButton = appointment.Date > DateTime.Now.AddDays(1);
            // assert
            Assert.True(showButton);
        }
    }
}
