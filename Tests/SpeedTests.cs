using System;
using System.Collections.Generic;
using System.Text;
using Units;
using Xunit;

namespace Tests
{
    public class SpeedTests
    {
        [Fact]
        public void MphToKph()
        {
            var speed70Mph = Speed.Create(70m, Speed.Mph);
            var expectedSpeedKph = Speed.Create(112.65408m, Speed.Kph);

            var actualSpeedKph = speed70Mph.ConvertTo(Speed.Kph);

            Assert.Equal(expectedSpeedKph, actualSpeedKph);
            Assert.Equal(expectedSpeedKph, speed70Mph);
        }

        [Fact]
        public void MphToMs()
        {
            var speed70Mph = Speed.Create(70m, Speed.Mph);
            var expectedSpeedMs = Speed.Create(31.2928m, Speed.Ms);

            var actualSpeedMs = speed70Mph.ConvertTo(Speed.Ms);

            Assert.Equal(expectedSpeedMs, actualSpeedMs);
            Assert.Equal(expectedSpeedMs, speed70Mph);
        }

        [Fact]
        public void FeetPerSecondToKpm()
        {
            var kmPerMinute = Speed.Create(Distance.Kilometer, Time.Minute);

            var speed13Fps = Speed.Create(Distance.Create(13.6m, Distance.Feet), Time.Second);
            var expectedSpeedKpm = Speed.Create(0.2487168m, kmPerMinute);

            var actualSpeedKpm = speed13Fps.ConvertTo(kmPerMinute);

            Assert.Equal(expectedSpeedKpm, actualSpeedKpm);
            Assert.Equal(expectedSpeedKpm, speed13Fps);
        }

        [Theory]
        [InlineData("100km", "h", "62.137119223733396961743418436mi", "h")]
        [InlineData("62.137119223733396961743418436mi", "h", "100km", "h")]
        [InlineData("100m", "s", "6000m", "m")]
        public void SpeedCreateAndEquals(string distanceInput, string unitTimeInput, string targetDistanceInput, string targetUnitTimeInput)
        {
            Distance.TryParse(distanceInput, out var distance);
            Time.TryParseUnit(unitTimeInput, out var time);

            var speed = Speed.Create(distance, time);

            Distance.TryParse(targetDistanceInput, out var targetDistance);
            Time.TryParseUnit(targetUnitTimeInput, out var targetTime);

            var targetSpeed = Speed.Create(targetDistance, targetTime);

            Assert.Equal(speed, targetSpeed);
        }

        [Theory]
        [InlineData("100km", "h", "62.137119223733396961743418436mi", "h")]
        [InlineData("62.137119223733396961743418436mi", "h", "100km", "h")]
        [InlineData("100m", "s", "6000m", "m")]
        public void SpeedCreateAndConvertTo(string distanceInput, string unitTimeInput, string targetDistanceInput, string targetUnitTimeInput)
        {
            Distance.TryParse(distanceInput, out var distance);
            Time.TryParseUnit(unitTimeInput, out var time);

            var speed = Speed.Create(distance, time);

            Distance.TryParse(targetDistanceInput, out var targetDistance);
            Time.TryParseUnit(targetUnitTimeInput, out var targetTime);

            var targetSpeed = Speed.Create(targetDistance, targetTime);

            Assert.Equal(speed, targetSpeed);
        }
    }
}
