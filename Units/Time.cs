using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Units
{
    public class Time : IEquatable<Time>
    {
        public static Time Millisecond = new Time(1m, 0.001m, "ms", "Millisecond");
        public static Time Second = new Time(1m, 1m, "s", "Second");
        public static Time Minute = new Time(1m, 60m * Second.Ratio, "m", "Minute");
        public static Time Hour = new Time(1m, 60m * Minute.Ratio, "h", "Hour");
        public static Time Day = new Time(1m, 24m * Hour.Ratio, "d", "Day");
        public static Time Year = new Time(1m, 365m * Day.Ratio, "y", "Year");

        public static Time Create(decimal value, Time unit) =>
            new Time(value, unit.Ratio, unit.ShortName, unit.LongName);

        private Time(decimal value, decimal ratio, string shortName, string longName)
        {
            Value = value;
            Ratio = ratio;
            ShortName = shortName;
            LongName = longName;
            ValueInSeconds = value * ratio;
        }

        public decimal Value { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public override string ToString() => $"{Value:G29}{ShortName}";
        public string ToLongString() => $"{Value:G29}{ShortName} ({LongName})";
        
        private decimal Ratio { get; }
        private decimal ValueInSeconds { get; }

        public static readonly IEnumerable<Time> AllUnits = new[]
        {
            Millisecond,
            Second,
            Minute,
            Hour,
            Day,
            Year
        };

        public Time Unit => new Time(1m, Ratio, ShortName, LongName);

        public Time ConvertTo(Time target)
        {
            var targetTime = ValueInSeconds / target.Ratio;

            return Create(targetTime, target);
        }

        public static bool TryParseUnit(string input, out Time value)
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

        private static readonly Regex Parser = new Regex(@"^(?<duration>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>\w+)$");

        public static bool TryParse(string input, out Time duration)
        {
            var decimalPart = Parser.Match(input).Groups["duration"];
            var unitPart = Parser.Match(input).Groups["unit"];

            var valueStringMatched = decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = TryParseUnit(unitPart.Value, out var unit);

            if (!valueStringMatched || !unitStringMatched)
            {
                duration = null;
                return false;
            }

            duration = Create(value, unit);

            return true;
        }

        public override int GetHashCode() => ValueInSeconds.GetHashCode();

        public bool Equals(Time other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ValueInSeconds == other.ValueInSeconds;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Time)obj);
        }

        public Time ScaleTo(Time targetUnit)
        {
            return new Time(Value * targetUnit.Ratio, targetUnit.Ratio, targetUnit.ShortName, targetUnit.LongName);
        }
    }
}
