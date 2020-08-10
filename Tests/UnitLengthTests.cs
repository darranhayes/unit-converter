using Units;
using Xunit;

namespace Tests
{
    public class UnitLengthTests
    {
        [Fact]
        public void CheckMeters()
        {
            var meter = UnitLength.Meter;
            Assert.Equal(1m, meter.Value);
            Assert.Equal(1m, meter.ValueInMeters);
        }

        [Fact]
        public void CheckKilometer()
        {
            var kilometer = UnitLength.Kilometer;
            Assert.Equal(1m, kilometer.Value);
            Assert.Equal(1000m, kilometer.ValueInMeters);
        }

        [Fact]
        public void CheckMile()
        {
            var mile = UnitLength.Mile;
            Assert.Equal(1m, mile.Value);
            Assert.Equal(1609.344m, mile.ValueInMeters);
        }
    }
}
