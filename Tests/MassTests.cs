using Units;
using Xunit;

namespace Tests
{
    public class MassTests
    {
        [Fact]
        public void MassTest1()
        {
            var x = Mass.Create(10m, Mass.Kilogram);
            var y = x.ConvertTo(Mass.Gram);

            Assert.Equal(Mass.Create(10_000m, Mass.Gram), y);
        }
    }
}
