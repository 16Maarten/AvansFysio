using AvansFysio.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AvansFysio.tests
{
    public class BR5
    {
        [Theory]
        [InlineData(14, false)]
        [InlineData(15, false)]
        [InlineData(16, true)]
        [InlineData(17, true)]
        public void AddPatientAgeCheck(int age, bool shouldPass)
        {
            // Arrange
            DateTime birthday = DateTime.Now.AddYears(-age);
            MinimumAge sut = new MinimumAge(16);

            //Act
            bool result = sut.IsValid(birthday);

            //Assert
            Assert.Equal(result, shouldPass);
        }
    }
}
