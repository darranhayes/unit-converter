using System;
using System.Linq;
using Units;

namespace UnitConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var availableUnits = string.Join(", ", UnitLength.AllUnits.Select(unit => unit.ShortName));

            while (true)
            {
                Console.WriteLine($"Enter your base unit in ({availableUnits})");
                var validInputUnit = UnitLength.TryParse(Console.ReadLine(), out var baseLengthUnit);
                if (!validInputUnit) continue;

                Console.WriteLine("Enter your distance in above unit");
                var validDistance = decimal.TryParse(Console.ReadLine(), out var distanceAmount);
                if (!validDistance) continue;

                Console.WriteLine($"Enter your target unit in ({availableUnits})");
                var validTargetUnit = UnitLength.TryParse(Console.ReadLine(), out var targetLengthUnit);
                if (!validTargetUnit) continue;

                var targetDistance = Distance.Create(distanceAmount, baseLengthUnit).ConvertTo(targetLengthUnit);

                Console.WriteLine($"\nYour target distance is: {targetDistance.ToLongString()}\n");
            }
        }
    }
}
