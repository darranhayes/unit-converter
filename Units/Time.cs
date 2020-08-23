﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Units
{
    public class Second : Time
    {
        public static Second Create(decimal seconds) => new Second(seconds);

        protected Second(decimal seconds) : base(seconds, 1m, Time.Second.ShortName, Time.Second.LongName, Time.Second.Aliases) { }
    }

    public class Time : UnitOfMeasure<Time>
    {
        public static Time Millisecond = new Time(1m, 0.001m, "ms", "Millisecond");
        public static Time Second = new Time(1m, 1m, "s", "Second");
        public static Time Minute = new Time(1m, 60m * Second.Ratio, "m", "Minute");
        public static Time Hour = new Time(1m, 60m * Minute.Ratio, "h", "Hour");
        public static Time Day = new Time(1m, 24m * Hour.Ratio, "d", "Day");
        public static Time Year = new Time(1m, 365m * Day.Ratio, "y", "Year");

        public static readonly IEnumerable<Time> AllUnits = new[]
        {
            Millisecond,
            Second,
            Minute,
            Hour,
            Day,
            Year
        };

        public static Time Create(decimal value, Time unit) =>
            new Time(value, unit.Ratio, unit.ShortName, unit.LongName, unit.Aliases);

        protected Time(decimal value, decimal ratio, string shortName, string longName, params string[] aliases) :
            base(value, shortName, longName, value * ratio, aliases)
        {
            Ratio = ratio;
        }

        public override Time Unit => new Time(1m, Ratio, ShortName, LongName, Aliases);

        private decimal Ratio { get; }

        public override Time ConvertTo(Time target)
        {
            if (Unit.Equals(target.Unit))
                return this;

            var targetTime = ValueInBaseUnit / target.Ratio;

            return Create(targetTime, target);
        }

        public static bool TryParseUnit(string input, out Time unitTime)
        {
            var lowerInput = input.ToLower();
            unitTime = null;

            var matchedUnit = AllUnits.FirstOrDefault(unit => unit.ShortName.ToLowerInvariant() == lowerInput || unit.LongName.ToLowerInvariant() == lowerInput);

            if (matchedUnit == null)
                return false;

            unitTime = matchedUnit;
            return true;
        }

        public static bool TryParse(string input, out Time duration)
        {
            duration = null;

            var decimalPart = Parser.Match(input).Groups["duration"];
            var unitPart = Parser.Match(input).Groups["unit"];

            var valueStringMatched = decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = TryParseUnit(unitPart.Value, out var unit);

            if (!valueStringMatched || !unitStringMatched)
                return false;

            duration = Create(value, unit);
            return true;
        }

        private static readonly Regex Parser = new Regex(@"^(?<duration>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>\w+)$");
    }
}
