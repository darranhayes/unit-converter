using System;
using System.Linq;
using Units;

namespace UnitConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var availableUnits = string.Join(", ", Distance.AllUnits.Select(unit => unit.ShortName));

            while (true)
            {
                Console.WriteLine($"Enter your base distance unit from ({availableUnits})");
                var validInputUnit = Distance.TryParseUnit(Console.ReadLine(), out var baseLengthUnit);
                if (!validInputUnit) continue;

                Console.WriteLine("Enter your distance in above unit");
                var validDistance = decimal.TryParse(Console.ReadLine(), out var distanceAmount);
                if (!validDistance) continue;

                Console.WriteLine($"Enter your target unit from ({availableUnits})");
                var validTargetUnit = Distance.TryParseUnit(Console.ReadLine(), out var targetLengthUnit);
                if (!validTargetUnit) continue;

                var targetDistance = Distance.Create(distanceAmount, baseLengthUnit).ConvertTo(targetLengthUnit);

                Console.WriteLine($"\nYour target distance is: {targetDistance.ToLongString()}\n");
            }
        }
    }
}
