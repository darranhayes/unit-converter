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
        public void ExploringSpeed()
        {
            var currentSpeed = Speed.Create(Distance.Create(100m, Distance.Kilometer), Time.Hour);

            var distanceTraveled = currentSpeed.GetDistanceAfter(Time.Create(2m, Time.Hour));
            var timeTaken = currentSpeed.GetTimeAfter(Distance.Create(200m, Distance.Kilometer));
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
