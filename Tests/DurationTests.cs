using System;
using System.Text.Json;
using Units;
using Xunit;

namespace Tests
{
    public class DurationTests
    {
        /// <summary>
        /// Show that converting duration in one unit of time equals the same real duration expressed in a different unit of time
        /// </summary>
        /// <param name="baseDurationInput"></param>
        /// <param name="baseUnitInput"></param>
        /// <param name="targetDurationInput"></param>
        /// <param name="targetUnitInput"></param>
        [Theory]
        [InlineData(1000, "msec", 1, "s")]
        [InlineData(60, "s", 1, "m")]
        [InlineData(60, "m", 1, "h")]
        [InlineData(24, "h", 1, "d")]
        [InlineData(365, "d", 1, "y")]
        [InlineData(1, "s", 1000, "msec")]
        [InlineData(1, "m", 60, "s")]
        [InlineData(1, "h", 60, "m")]
        [InlineData(1, "d", 24, "h")]
        [InlineData(1, "y", 365, "d")]
        public void ConvertedDurationsAreEqual(
            decimal baseDurationInput, string baseUnitInput, 
            decimal targetDurationInput, string targetUnitInput
            )
        {
            Time.TryParse(baseUnitInput, out var @base);
            var baseDistance = Duration.Create(baseDurationInput, @base);

            Time.TryParse(targetUnitInput, out var targetUnit);
            var targetDistance = baseDistance.ConvertTo(targetUnit);

            Assert.Equal(baseUnitInput, baseDistance.Unit.ToString());
            Assert.Equal(baseDurationInput, baseDistance.Value);

            Assert.Equal(targetUnitInput, targetDistance.Unit.ToString());
            Assert.Equal(targetDurationInput, targetDistance.Value);
            
            Assert.Equal(baseDistance, targetDistance);
        }

        /// <summary>
        /// Rudimentary verification that Duration.Equals does not always return true
        /// </summary>
        [Fact]
        public void CheckNotEquals()
        {
            Time.TryParse("m", out var @base);

            var baseDuration = Duration.Create(5m, @base);

            Time.TryParse("s", out var targetUnit);

            var targetDuration = Duration.Create(5m, targetUnit);

            Assert.Equal(baseDuration.Value, targetDuration.Value);
            Assert.NotEqual(baseDuration.Unit, targetDuration.Unit);
            Assert.NotEqual(baseDuration, targetDuration);
        }
    }
}
