using FluentAssertions;
using NUnit.Framework;
using WeatherStation.Validation;

namespace WeatherStation.Test.Validation
{
    [TestFixture]
    public class StringToDateValidationRuleTest
    {
        [Test]
        public void Validate_ReturnsTrueOnValidDateString()
        {
            const string validTime = "12.05.2008";
            var cut = new StringToDateValidationRule();

            var result = cut.Validate(validTime, null);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_ReturnsFalseOnInvalidDateString()
        {
            const string invalidTime = "#äö+üp";
            var cut = new StringToDateValidationRule();

            var result = cut.Validate(invalidTime, null);

            result.IsValid.Should().BeFalse();
        }
    }
}
