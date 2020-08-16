using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Units
{
    public class Distance : IEquatable<Distance>
    {
        public static Distance Millimeter = new Distance(1m, 0.001m, "mm", "Millimeter");
        public static Distance Centimeter = new Distance(1m, 0.01m, "cm", "Centimeter");
        public static Distance Inch = new Distance(1m, 0.0254m, "i", "Inch");
        public static Distance Foot = new Distance(1m, 12m * Inch.Ratio, "ft", "Foot");
        public static Distance Yard = new Distance(1m, 3m * Foot.Ratio, "yd", "Yard");
        public static Distance Meter = new Distance(1m, 1m, "m", "Meter");
        public static Distance Kilometer = new Distance(1m, 1000m, "km", "Kilometer");
        public static Distance Mile = new Distance(1m, 1609.344m, "mi", "Mile");

        public static Distance Create(decimal value, Distance unit) =>
            new Distance(value, unit.Ratio, unit.ShortName, unit.LongName);

        private Distance(decimal value, decimal ratio, string shortName, string longName)
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

        public static readonly IEnumerable<Distance> AllUnits = new[]
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

        public Distance ConvertTo(Distance target)
        {
            var targetDistance = ValueInMeters / target.Ratio;

            return Create(targetDistance, target);
        }

        public static bool TryParseUnit(string input, out Distance unitDistance)
        {
            var lowerInput = input.ToLower();

            var matchedUnit = AllUnits.FirstOrDefault(unit => unit.ShortName.ToLowerInvariant() == lowerInput || unit.LongName.ToLowerInvariant() == lowerInput);
            
            if (matchedUnit == null)
            {
                unitDistance = null;
                return false;
            }

            unitDistance = matchedUnit;
            return true;
        }

        private static readonly Regex DistanceParser = new Regex(@"^(?<distance>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>\w+)$");

        public static bool TryParse(string input, out Distance distance)
        {
            var decimalPart = DistanceParser.Match(input).Groups["distance"];
            var unitPart = DistanceParser.Match(input).Groups["unit"];

            var valueStringMatched = Decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = Distance.TryParseUnit(unitPart.Value, out var unit);

            if (!valueStringMatched || !unitStringMatched)
            {
                distance = null;
                return false;
            }

            distance = Create(value, unit);
            return true;
        }

        public override int GetHashCode()
        {
            return ValueInMeters.GetHashCode();
        }

        public bool Equals(Distance other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ValueInMeters == other.ValueInMeters;
        }
    }
}
