using Units;
using Xunit;

namespace Tests
{
    public class DistanceTests
    {
        [Fact]
        public void KilometerViaMeters()
        {
            var kilometers = Distance.Create(0.5m, UnitLength.Kilometer);

            var backToKilometers =
                kilometers
                    .ConvertTo(UnitLength.Meter)
                    .ConvertTo(UnitLength.Kilometer);

            Assert.Equal(kilometers.Value, backToKilometers.Value);
        }

        [Fact]
        public void MilesViaMeters()
        {
            var miles = Distance.Create(0.5m, UnitLength.Mile);

            var backToMiles =
                miles
                    .ConvertTo(UnitLength.Meter)
                    .ConvertTo(UnitLength.Mile);

            Assert.Equal(miles.Value, backToMiles.Value);
        }

        [Fact]
        public void MilesViaKilometers()
        {
            var miles = Distance.Create(0.5m, UnitLength.Mile);

            var backToMiles =
                miles
                    .ConvertTo(UnitLength.Kilometer)
                    .ConvertTo(UnitLength.Mile);

            Assert.Equal(miles.Value, backToMiles.Value);
        }

        [Fact]
        public void KilometersViaMillimeters()
        {
            var kilometers = Distance.Create(5m, UnitLength.Kilometer);

            var backToKilometers =
                kilometers
                    .ConvertTo(UnitLength.Millimeter)
                    .ConvertTo(UnitLength.Kilometer);

            Assert.Equal(kilometers.Value, backToKilometers.Value);
        }

        [Fact]
        public void MilesViaInches()
        {
            var miles = Distance.Create(5.0m, UnitLength.Mile);

            var backToMiles =
                miles
                    .ConvertTo(UnitLength.Inch)
                    .ConvertTo(UnitLength.Mile);

            Assert.Equal(miles.Value, backToMiles.Value);
        }
    }
}