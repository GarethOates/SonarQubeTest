using SonarQubeTest;
using System;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(1, 5, 6)]
        [InlineData(40, 5, 45)]
        public void Addition_Adds_Two_Numbers(int a, int b, int expected)
        {
            // Arrange
            var calc = new Calculator();

            // Act
            int actual = calc.Addition(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(1, 5, -4)]
        [InlineData(40, 5, 35)]
        public void Subtraction_Subtracts_Two_Numbers(int a, int b, int expected)
        {
            // Arrange
            var calc = new Calculator();

            // Act
            int actual = calc.Subtraction(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
