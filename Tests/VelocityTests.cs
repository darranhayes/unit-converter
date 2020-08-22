using System;
using Units;
using Xunit;

namespace Tests
{
    public class VelocityTests
    {
        [Fact]
        public void MphToKph()
        {
            var velocity70Mph = Velocity.Create(70m, Velocity.Mph);
            var expectedVelocityKph = Velocity.Create(112.65408m, Velocity.Kph);

            var actualVelocityKph = velocity70Mph.ConvertTo(Velocity.Kph);

            Assert.Equal(expectedVelocityKph, actualVelocityKph);
            Assert.Equal(expectedVelocityKph, velocity70Mph);
        }

        [Fact]
        public void MphToMs()
        {
            var velocity70Mph = Velocity.Create(70m, Velocity.Mph);
            var expectedVelocityMs = Velocity.Create(31.2928m, Velocity.Ms);

            var actualVelocityMs = velocity70Mph.ConvertTo(Velocity.Ms);

            Assert.Equal(expectedVelocityMs, actualVelocityMs);
            Assert.Equal(expectedVelocityMs, velocity70Mph);
        }

        [Fact]
        public void FeetPerSecondToKpm()
        {
            var kmPerMinute = Velocity.Create(Distance.Kilometer, Time.Minute);

            var velocity13Fps = Velocity.Create(Distance.Create(13.6m, Distance.Feet), Time.Second);
            var expectedVelocityKpm = Velocity.Create(0.2487168m, kmPerMinute);

            var actualVelocityKpm = velocity13Fps.ConvertTo(kmPerMinute);

            Assert.Equal(expectedVelocityKpm, actualVelocityKpm);
            Assert.Equal(expectedVelocityKpm, velocity13Fps);
        }

        [Fact]
        public void DistanceTraveled()
        {
            var kmh = Velocity.Create(70m, Velocity.Kph);

            var distance = kmh.GetDistanceAfter(Time.Create(5m, Time.Hour));

            Assert.Equal(Distance.Create(350m, Distance.Kilometer), distance);
        }

        [Fact]
        public void TimeTaken()
        {
            var kmh = Velocity.Create(70m, Velocity.Kph);

            var duration = kmh.GetTimeTaken(Distance.Create(350m, Distance.Kilometer));

            Assert.Equal(Time.Create(5, Time.Hour), duration);
        }

        [Fact]
        public void Accelerate()
        {
            var currentVelocity = Velocity.Create(0m, Velocity.Ms);

            var newVelocity = Acceleration.Accelerate(Meter.Create(10m), Second.Create(3m), currentVelocity);

            Assert.Equal(Velocity.Create(30m, Velocity.Ms), newVelocity);
        }

        [Theory]
        [InlineData("70mph", "km / h", "112.65408kmh")]
        [InlineData("100kmh", "mi / h", "62.137119223733396961743418436mi / h")]
        [InlineData("62.137119223733396961743418436mi / h", "kph", "100km / h")]
        [InlineData("100ms", "m / m", "6000m / m")]
        [InlineData("100ms", "m / s", "100m / s")]
        public void VelocityConversions(string velocityInput, string targetUnitInput, string expectedConvertedVelocityInput)
        {
            Velocity.TryParseUnit(targetUnitInput, out var targetUnit);
            Velocity.TryParse(velocityInput, out var velocity);
            Velocity.TryParse(expectedConvertedVelocityInput, out var expectedVelocity);

            Assert.NotNull(targetUnit);
            Assert.NotNull(velocity);
            Assert.NotNull(expectedVelocity);

            var actualConvertedVelocity = velocity.ConvertTo(targetUnit);

            Assert.Equal(expectedVelocity, actualConvertedVelocity);
        }

        [Theory]
        [InlineData("100km", "h", "62.137119223733396961743418436mi", "h")]
        [InlineData("62.137119223733396961743418436mi", "h", "100km", "h")]
        [InlineData("100m", "s", "6000m", "m")]
        public void VelocityCreateAndEquals(string distanceInput, string unitTimeInput, string targetDistanceInput, string targetUnitTimeInput)
        {
            Distance.TryParse(distanceInput, out var distance);
            Time.TryParseUnit(unitTimeInput, out var time);

            var velocity = Velocity.Create(distance, time);

            Distance.TryParse(targetDistanceInput, out var targetDistance);
            Time.TryParseUnit(targetUnitTimeInput, out var targetTime);

            var targetVelocity = Velocity.Create(targetDistance, targetTime);

            Assert.Equal(velocity, targetVelocity);
        }

        [Theory]
        [InlineData("100km", "h", "62.137119223733396961743418436mi", "h")]
        [InlineData("62.137119223733396961743418436mi", "h", "100km", "h")]
        [InlineData("100m", "s", "6000m", "m")]
        public void VelocityCreateAndConvertTo(string distanceInput, string unitTimeInput, string targetDistanceInput, string targetUnitTimeInput)
        {
            Distance.TryParse(distanceInput, out var distance);
            Time.TryParseUnit(unitTimeInput, out var time);

            var velocity = Velocity.Create(distance, time);

            Distance.TryParse(targetDistanceInput, out var targetDistance);
            Time.TryParseUnit(targetUnitTimeInput, out var targetTime);

            var targetVelocity = Velocity.Create(targetDistance, targetTime);

            Assert.Equal(velocity, targetVelocity);
        }
    }
}
