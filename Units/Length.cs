using System;
using System.Collections.Generic;
using System.Linq;

namespace Units
{
    public class Length : IEquatable<Length>
    {
        public static Length Millimeter = new Length(1m, 0.001m, "mm", "Millimeter");
        public static Length Centimeter = new Length(1m, 0.01m, "cm", "Centimeter");
        public static Length Inch = new Length(1m, 0.0254m, "i", "Inch");
        public static Length Foot = new Length(1m, 12m * Inch.Ratio, "ft", "Foot");
        public static Length Yard = new Length(1m, 3m * Foot.Ratio, "yd", "Yard");
        public static Length Meter = new Length(1m, 1m, "m", "Meter");
        public static Length Kilometer = new Length(1m, 1000m, "km", "Kilometer");
        public static Length Mile = new Length(1m, 1609.344m, "mi", "Mile");

        public static Length Create(decimal value, Length unit) =>
            new Length(value, unit.Ratio, unit.ShortName, unit.LongName);

        private Length(decimal value, decimal ratio, string shortName, string longName)
        {
            Value = value;
            Ratio = ratio;
            ShortName = shortName;
            LongName = longName;
            ValueInMeters = value * ratio;
        }

        public decimal Value { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public override string ToString() => $"{Value:G29}{ShortName}";
        public string ToLongString() => $"{Value:G29}{ShortName} ({LongName})";

        private decimal Ratio { get; }
        private decimal ValueInMeters { get; }

        public static readonly IEnumerable<Length> AllUnits = new[]
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

        public Length ConvertTo(Length target)
        {
            var targetDistance = ValueInMeters / target.Ratio;

            return Create(targetDistance, target);
        }

        public static bool TryParseUnit(string input, out Length value)
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

        public override int GetHashCode()
        {
            return ValueInMeters.GetHashCode();
        }

        public bool Equals(Length other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ValueInMeters == other.ValueInMeters;
        }
    }
}
