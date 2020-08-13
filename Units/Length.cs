using System.Collections.Generic;
using System.Linq;

namespace Units
{
    public class Length
    {
        public static Length Millimeter = new Length(0.001m, "mm", "Millimeter");
        public static Length Centimeter = new Length(0.01m, "cm", "Centimeter");
        public static Length Inch = new Length(0.0254m, "i", "Inch");
        public static Length Foot = new Length(12m * Inch.Ratio, "ft", "Foot");
        public static Length Yard = new Length(3m * Foot.Ratio, "yd", "Yard");
        public static Length Meter = new Length(1m, "m", "Meter");
        public static Length Kilometer = new Length(1000m, "km", "Kilometer");
        public static Length Mile = new Length(1609.344m, "mi", "Mile");

        private Length(decimal ratio, string shortName, string longName)
        {
            Value = 1m;
            Ratio = ratio;
            ShortName = shortName;
            LongName = longName;
            ValueInMeters = 1m * ratio;
        }

        static Length()
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
        public decimal Ratio { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public static IEnumerable<Length> AllUnits { get; }
        public override string ToString() => $"{ShortName}";
        public string ToLongString() => $"{ShortName} ({LongName})";
        private decimal ValueInMeters { get; }

        public static bool TryParse(string input, out Length value)
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

        public override bool Equals(object obj)
        {
            var unit = obj as Length;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Length other) => ValueInMeters == other.ValueInMeters;

        public override int GetHashCode() => ValueInMeters.GetHashCode();
    }
}
