using Units;
using Xunit;

namespace Tests
{
    public class TimeTests
    {
        /// <summary>
        /// Time.Create - Show that converting time in one unit equals the same real time expressed in a different unit
        /// </summary>
        /// <param name="timeInput"></param>
        /// <param name="unitInput"></param>
        /// <param name="targetTimeInput"></param>
        /// <param name="targetTimeUnitInput"></param>
        [Theory]
        [InlineData(1000, "ms", 1, "s")]
        [InlineData(60, "s", 1, "m")]
        [InlineData(60, "m", 1, "h")]
        [InlineData(24, "h", 1, "d")]
        [InlineData(365, "d", 1, "y")]
        [InlineData(1, "s", 1000, "ms")]
        [InlineData(1, "m", 60, "s")]
        [InlineData(1, "h", 60, "m")]
        [InlineData(1, "d", 24, "h")]
        [InlineData(1, "y", 365, "d")]
        public void TimeCreateAndConvertTo(
            decimal timeInput, string unitInput,
            decimal targetTimeInput, string targetTimeUnitInput
        )
        {
            Time.TryParseUnit(unitInput, out var @base);
            var baseTime = Time.Create(timeInput, @base);

            Time.TryParseUnit(targetTimeUnitInput, out var targetUnit);
            var targetTime = baseTime.ConvertTo(targetUnit);

            Assert.Equal($"{timeInput}{unitInput}", baseTime.ToString());
            Assert.Equal(timeInput, baseTime.Value);

            Assert.Equal($"{targetTimeInput}{targetTimeUnitInput}", targetTime.ToString());
            Assert.Equal(targetTimeInput, targetTime.Value);

            Assert.Equal(baseTime, targetTime);
        }

        /// <summary>
        /// Rudimentary verification that Time.Equals does not always return true
        /// </summary>
        [Fact]
        public void TimeCreateCheckNotEqual()
        {
            Time.TryParseUnit("m", out var mm);
            var time = Time.Create(9m, mm);

            Time.TryParseUnit("s", out var cm);
            var targetTime = Time.Create(9m, cm);

            Assert.Equal("9m", time.ToString());
            Assert.Equal("9s", targetTime.ToString());
            Assert.NotEqual(time, targetTime);
        }

        /// <summary>
        /// Time.TryParse - Show that converting time in one unit of length equals the same real time expressed
        /// in a different unit of length
        /// </summary>
        /// <param name="baseTimeInput"></param>
        /// <param name="targetTimeInput"></param>
        [Theory]
        [InlineData("1000ms", "1s")]
        [InlineData("60s", "1m")]
        [InlineData("60m", "1h")]
        [InlineData("24h", "1d")]
        [InlineData("365d", "1y")]
        [InlineData("1s", "1000ms")]
        [InlineData("1m", "60s")]
        [InlineData("1h", "60m")]
        [InlineData("1d", "24h")]
        [InlineData("1y", "365d")]
        public void TimeTryParseAndEquals(
            string baseTimeInput,
            string targetTimeInput
        )
        {
            Time.TryParse(baseTimeInput, out var time);

            Time.TryParse(targetTimeInput, out var targetTime);

            Assert.Equal(time, targetTime);
        }

        /// <summary>
        /// Time.TryParse - Show that converting time in one unit of length equals the same real time expressed
        /// in a different unit of length
        /// </summary>
        /// <param name="baseTimeInput"></param>
        /// <param name="targetTimeInput"></param>
        [Theory]
        [InlineData("60 s", "1m")]
        [InlineData("60  s", "1m")]
        [InlineData("60   s", "1m")]
        [InlineData("60\ts", "1m")]
        public void TimeTryParseWithWhitespace(
            string baseTimeInput,
            string targetTimeInput
        )
        {
            Time.TryParse(baseTimeInput, out var time);

            Time.TryParse(targetTimeInput, out var targetTime);

            Assert.Equal(time, targetTime);
        }

        /// <summary>
        /// Rudimentary verification that Time.Equals does not always return true
        /// </summary>
        [Fact]
        public void TimeTryParseCheckNotEqual()
        {
            Time.TryParse("9s", out var mm);

            Time.TryParse("9m", out var cm);

            Assert.Equal("9s", mm.ToString());
            Assert.Equal("9m", cm.ToString());
            Assert.NotEqual(mm, cm);
        }
    }
}
