﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Units
{

    public class Meter : Distance
    {
        public static Meter Create(decimal meters) => new Meter(meters);

        protected Meter(decimal meters) : base(meters, 1m, Distance.Meter.ShortName, Distance.Meter.LongName, Distance.Meter.Aliases) { }
    }

    public class Distance : UnitOfMeasure<Distance>
    {
        public static Distance Millimeter = new Distance(1m, 0.001m, "mm", "Millimeter");
        public static Distance Centimeter = new Distance(1m, 0.01m, "cm", "Centimeter");
        public static Distance Inch = new Distance(1m, 0.0254m, "i", "Inch");
        public static Distance Feet = new Distance(1m, 12m * Inch.Ratio, "ft", "Feet");
        public static Distance Yard = new Distance(1m, 3m * Feet.Ratio, "yd", "Yard");
        public static Distance Meter = new Distance(1m, 1m, "m", "Meter");
        public static Distance Kilometer = new Distance(1m, 1000m, "km", "Kilometer");
        public static Distance Mile = new Distance(1m, 1609.344m, "mi", "Mile");

        public static readonly IEnumerable<Distance> AllUnits = new[]
        {
            Millimeter,
            Centimeter,
            Inch,
            Feet,
            Yard,
            Meter,
            Kilometer,
            Mile
        };

        public static Distance Create(decimal value, Distance unit) =>
            new Distance(value, unit.Ratio, unit.ShortName, unit.LongName, unit.Aliases);

        protected Distance(decimal value, decimal ratio, string shortName, string longName, params string[] aliases) :
            base(value, shortName, longName, value * ratio, aliases)
        {
            Ratio = ratio;
        }

        public override Distance ConvertTo(Distance target)
        {
            if (Unit.Equals(target.Unit))
                return this;

            var targetDistance = ValueInBaseUnit / target.Ratio;

            return Create(targetDistance, target);
        }

        public override Distance Unit => new Distance(1m, Ratio, ShortName, LongName, Aliases);

        private decimal Ratio { get; }

        public static bool TryParseUnit(string inputDistance, out Distance unitDistance)
        {
            var input = inputDistance.ToLower();
            unitDistance = null;

            var matchedUnit = AllUnits.FirstOrDefault(unit => unit.ShortName.ToLowerInvariant() == input || unit.LongName.ToLowerInvariant() == input);
            
            if (matchedUnit == null)
                return false;

            unitDistance = matchedUnit;
            return true;
        }

        public static bool TryParse(string input, out Distance distance)
        {
            distance = null;

            var decimalPart = Parser.Match(input).Groups["distance"];
            var unitPart = Parser.Match(input).Groups["unit"];

            var valueStringMatched = decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = TryParseUnit(unitPart.Value, out var unit);

            if (!valueStringMatched || !unitStringMatched)
                return false;

            distance = Create(value, unit);
            return true;
        }

        private static readonly Regex Parser = new Regex(@"^(?<distance>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>\w+)$");
    }
}
