using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
    public class Time
    {
        public static Time Millisecond = new Time(0.001m, "msec", "Millisecond");
        public static Time Second = new Time(1m, "s", "Second");
        public static Time Minute = new Time(60m * Second.Ratio, "m", "Minute");
        public static Time Hour = new Time(60m * Minute.Ratio, "h", "Hour");
        public static Time Day = new Time(24m * Hour.Ratio, "d", "Day");
        public static Time Year = new Time(365m * Day.Ratio, "y", "Year");

        private Time(decimal ratio, string shortName, string longName)
        {
            Value = 1m;
            Ratio = ratio;
            ShortName = shortName;
            LongName = longName;
            ValueInSeconds = 1m * ratio;
        }

        static Time()
        {
            AllUnits = new[]
            {
                Millisecond,
                Second,
                Minute,
                Hour,
                Day,
                Year
            };
        }

        public decimal Value { get; }
        public decimal Ratio { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public static IEnumerable<Time> AllUnits { get; }
        public override string ToString() => $"{ShortName}";
        public string ToLongString() => $"{ShortName} ({LongName})";
        private decimal ValueInSeconds { get; }

        public static bool TryParse(string input, out Time value)
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
            var unit = obj as Time;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Time other) => ValueInSeconds == other.ValueInSeconds;

        public override int GetHashCode() => ValueInSeconds.GetHashCode();
    }
}
