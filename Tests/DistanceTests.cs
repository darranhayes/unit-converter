using System;
using System.Text.Json;
using Units;
using Xunit;

namespace Tests
{
    public class DistanceTests
    {
        /// <summary>
        /// Distance.Create - Show that converting distance in one unit equals the same real distance expressed in a different unit
        /// </summary>
        /// <param name="distanceInput"></param>
        /// <param name="unitInput"></param>
        /// <param name="targetDistanceInput"></param>
        /// <param name="targetDistanceUnitInput"></param>
        [Theory]
        [InlineData(10, "mm", 1, "cm")]
        [InlineData(100, "cm", 1, "m")]
        [InlineData(1000, "mm", 1, "m")]
        [InlineData(1000, "m", 1, "km")]
        [InlineData(12, "i", 1, "ft")]
        [InlineData(3, "ft", 1, "yd")]
        [InlineData(1609.344, "m", 1, "mi")]
        [InlineData(1, "cm", 10, "mm")]
        [InlineData(1, "m", 100, "cm")]
        [InlineData(1, "m", 1000, "mm")]
        [InlineData(1, "km", 1000, "m")]
        [InlineData(1, "ft", 12, "i")]
        [InlineData(1, "yd", 3, "ft")]
        [InlineData(1, "mi", 1609.344, "m")]
        public void DistanceCreate(
            decimal distanceInput, string unitInput, 
            decimal targetDistanceInput, string targetDistanceUnitInput
            )
        {
            Distance.TryParseUnit(unitInput, out var @base);
            var baseDistance = Distance.Create(distanceInput, @base);

            Distance.TryParseUnit(targetDistanceUnitInput, out var targetUnit);
            var targetDistance = baseDistance.ConvertTo(targetUnit);

            Assert.Equal($"{distanceInput}{unitInput}", baseDistance.ToString());
            Assert.Equal(distanceInput, baseDistance.Value);

            Assert.Equal($"{targetDistanceInput}{targetDistanceUnitInput}", targetDistance.ToString());
            Assert.Equal(targetDistanceInput, targetDistance.Value);
            
            Assert.Equal(baseDistance, targetDistance);
        }

        /// <summary>
        /// Rudimentary verification that Distance.Equals does not always return true
        /// </summary>
        [Fact]
        public void DistanceCreateCheckNotEqual()
        {
            Distance.TryParseUnit("mm", out var mm);
            var distance = Distance.Create(9m, mm);

            Distance.TryParseUnit("cm", out var cm);
            var targetDistance = Distance.Create(9m, cm);

            Assert.Equal("9mm", distance.ToString());
            Assert.Equal("9cm", targetDistance.ToString());
            Assert.NotEqual(distance, targetDistance);
        }

        /// <summary>
        /// Distance.TryParse - Show that converting distance in one unit of length equals the same real distance expressed
        /// in a different unit of length
        /// </summary>
        /// <param name="baseDistanceInput"></param>
        /// <param name="targetDistanceInput"></param>
        [Theory]
        [InlineData("10mm", "1cm")]
        [InlineData("100cm", "1m")]
        [InlineData("1000mm", "1m")]
        [InlineData("1000m", "1km")]
        [InlineData("12i", "1ft")]
        [InlineData("3ft", "1yd")]
        [InlineData("1609.344m", "1mi")]
        [InlineData("1cm", "10mm")]
        [InlineData("1m", "100cm")]
        [InlineData("1m", "1000mm")]
        [InlineData("1km", "1000m")]
        [InlineData("1ft", "12i")]
        [InlineData("1yd", "3ft")]
        [InlineData("1mi", "1609.344m")]
        public void DistanceTryParse(
            string baseDistanceInput,
            string targetDistanceInput
        )
        {
            Distance.TryParse(baseDistanceInput, out var distance);

            Distance.TryParse(targetDistanceInput, out var targetDistance);

            Assert.Equal(distance, targetDistance);
        }

        /// <summary>
        /// Distance.TryParse - Show that converting distance in one unit of length equals the same real distance expressed
        /// in a different unit of length
        /// </summary>
        /// <param name="baseDistanceInput"></param>
        /// <param name="targetDistanceInput"></param>
        [Theory]
        [InlineData("10 mm", "1cm")]
        [InlineData("10  mm", "1cm")]
        [InlineData("10   mm", "1cm")]
        [InlineData("10\tmm", "1cm")]
        public void DistanceTryParseWithWhitespace(
            string baseDistanceInput,
            string targetDistanceInput
        )
        {
            Distance.TryParse(baseDistanceInput, out var distance);

            Distance.TryParse(targetDistanceInput, out var targetDistance);

            Assert.Equal(distance, targetDistance);
        }

        /// <summary>
        /// Rudimentary verification that Distance.Equals does not always return true
        /// </summary>
        [Fact]
        public void DistanceTryParseCheckNotEqual()
        {
            Distance.TryParse("9mm", out var mm);

            Distance.TryParse("9cm", out var cm);

            Assert.Equal("9mm", mm.ToString());
            Assert.Equal("9cm", cm.ToString());
            Assert.NotEqual(mm, cm);
        }
    }
}
