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
                Console.WriteLine($"Enter your base distance using these units: ({availableUnits}), e.g., 99.9km");
                var validInput = Distance.TryParse(Console.ReadLine(), out var baseLength);
                if (!validInput) continue;

                Console.WriteLine($"Enter your target unit from ({availableUnits})");
                var validTargetUnit = Distance.TryParseUnit(Console.ReadLine(), out var targetLengthUnit);
                if (!validTargetUnit) continue;

                var targetDistance = baseLength.ConvertTo(targetLengthUnit);

                Console.WriteLine($"\nYour target distance is: {targetDistance.ToLongString()}\n");
            }
        }
    }
}
