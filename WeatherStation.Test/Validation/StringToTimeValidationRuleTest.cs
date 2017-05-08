using FluentAssertions;
using NUnit.Framework;
using WeatherStation.Validation;

namespace WeatherStation.Test.Validation
{
    [TestFixture]
    public class StringToTimeValidationRuleTest
    {
        [Test]
        [TestCase("10:10")]
        [TestCase("04:32")]
        [TestCase("4:32")]
        public void Validate_ReturnsTrueOnValidTimeString(string validString)
        {
            var cut = new StringToTimeValidationRule();

            var result = cut.Validate(validString, null);

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_ReturnsFalseOnInvalidTimeString()
        {
            const string invalidTime = "#äö+üp";
            var cut = new StringToTimeValidationRule();

            var result = cut.Validate(invalidTime, null);

            result.IsValid.Should().BeFalse();
        }
    }
}
