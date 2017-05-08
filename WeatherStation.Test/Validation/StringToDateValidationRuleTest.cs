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
            const string validDate = "12.05.2008";
            var cut = new StringToDateValidationRule();

            var result = cut.Validate(validDate, null);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_ReturnsFalseOnInvalidDateString()
        {
            const string invalidDate = "#äö+üp";
            var cut = new StringToDateValidationRule();

            var result = cut.Validate(invalidDate, null);

            result.IsValid.Should().BeFalse();
        }
    }
}
