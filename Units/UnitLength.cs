using System.Collections.Generic;
using System.Linq;

namespace Units
{
    public class UnitLength
    {
        public static UnitLength Millimeter = new UnitLength(0.001m, "mm", "Millimeter");
        public static UnitLength Centimeter = new UnitLength(0.01m, "cm", "Centimeter");
        public static UnitLength Inch = new UnitLength(0.0254m, "i", "Inch");
        public static UnitLength Foot = new UnitLength(12m * Inch.Ratio, "ft", "Foot");
        public static UnitLength Yard = new UnitLength(3m * Foot.Ratio, "yd", "Yard");
        public static UnitLength Meter = new UnitLength(1m, "m", "Meter");
        public static UnitLength Kilometer = new UnitLength(1000m, "km", "Kilometer");
        public static UnitLength Mile = new UnitLength(1609.344m, "mi", "Mile");

        private UnitLength(decimal ratio, string shortName, string longName)
        {
            Value = 1m;
            Ratio = ratio;
            ShortName = shortName;
            LongName = longName;
            ValueInMeters = 1m * ratio;
        }

        static UnitLength()
        {
            AllUnits = new[]
            {
                Millimeter,
                Centimeter,
                Inch,
                Foot,
                Yard,
                Meter,
                Kilometer,
                Mile
            };
        }

        public decimal Value { get; }

        public decimal ValueInMeters { get; }

        public decimal Ratio { get; }

        public string ShortName { get; }

        public string LongName { get; }

        public static IEnumerable<UnitLength> AllUnits { get; }

        public static bool TryParse(string input, out UnitLength value)
        {
            var lowerInput = input.ToLower();

            var matchedUnit = AllUnits.FirstOrDefault(unit => unit.ShortName.ToLowerInvariant() == lowerInput || unit.LongName.ToLowerInvariant() == lowerInput);
            if (matchedUnit == null)
            {
                value = null;
                return false;
            }

            value = matchedUnit;
            return true;
        }

        public override string ToString()
        {
            return $"{ShortName}";
        }

        public string ToLongString()
        {
            return $"{ShortName} ({LongName})";
        }
    }
}
