using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Units
{
    public class Velocity : UnitOfMeasure<Velocity>
    {
        public static Velocity Mph = new Velocity(Distance.Mile, Time.Hour, "mph");
        public static Velocity Kph = new Velocity(Distance.Kilometer, Time.Hour, "kph", "kmh");
        public static Velocity Ms = new Velocity(Distance.Meter, Time.Second, "ms");

        public static Velocity Create(Distance distance, Time time)
        {
            return new Velocity(distance, time.Unit);
        }

        public static Velocity Create(decimal value, Velocity unitVelocity)
        {
            var distance = Distance.Create(value, unitVelocity.Distance);
            return Create(distance, unitVelocity.Time);
        }

        private Velocity(Distance distance, Time time, params string[] aliases) : base(distance.Value, $"{distance.Unit.ShortName} / {time.Unit.ShortName}", $"{distance.Unit.LongName} / {time.Unit.LongName}", aliases)
        {
            Distance = distance;
            Time = time;
        }

        public override string ToString() => $"{Distance.Value} {ShortName}";
        public override string ToLongString() => $"{Distance.Value} {LongName}";
        public override Velocity Unit => new Velocity(Distance.Unit, Time.Unit, Aliases);

        private Distance Distance { get; }
        private Time Time { get; }

        public override Velocity ConvertTo(Velocity target)
        {
            if (Unit.Equals(target.Unit))
                return this;

            var currentFactor = GetConversionFactor(this);
            var targetFactor = GetConversionFactor(target);

            var targetDistance = Distance.Value * currentFactor / targetFactor;

            return new Velocity(Distance.Create(targetDistance, target.Distance.Unit), target.Time.Unit);
        }

        public Distance GetDistanceAfter(Time travelingFor)
        {
            var duration = travelingFor.ConvertTo(Time);
            var distanceTraveled = Distance.Value * duration.Value;

            return Distance.Create(distanceTraveled, Distance);
        }

        public Time GetTimeTaken(Distance travelingFor)
        {
            var distance = travelingFor.ConvertTo(Distance);
            var timeTaken = distance.Value / Distance.Value;

            return Time.Create(timeTaken, Time);
        }

        public static readonly IEnumerable<Velocity> AllUnits = new[]
        {
            Mph,
            Kph,
            Ms
        };

        private static decimal GetConversionFactor(Velocity s)
        {
            var distance = s.Distance.Unit.ConvertTo(Distance.Meter).Value;
            var time = s.Time.Unit.ConvertTo(Time.Second).Value;

            return distance / time;
        }

        public static bool TryParseUnit(string unitName, out Velocity unitVelocity)
        {
            var input = unitName.ToLower();
            unitVelocity = null;

            bool AliasMatch(IEnumerable<string> aliases) => aliases.Any(a => a.ToLower() == input);

            var matchedUnit = AllUnits.FirstOrDefault(unit => unit.ShortName.ToLowerInvariant() == input || unit.LongName.ToLowerInvariant() == input || AliasMatch(unit.Aliases));

            if (matchedUnit == null)
            {
                var parts = input.Split("/");

                if (parts.Length < 2)
                    return false;

                var distanceUnitFound = Distance.TryParseUnit(parts[0].Trim(), out var distanceUnit);
                var timeUnitFound = Time.TryParseUnit(parts[1].Trim(), out var timeUnit);

                if (!distanceUnitFound || !timeUnitFound) return false;

                unitVelocity = Create(distanceUnit, timeUnit);
                return true;
            }

            unitVelocity = matchedUnit;
            return true;
        }

        public static bool TryParse(string inputVelocity, out Velocity velocity)
        {
            velocity = null;

            var decimalPart = Parser.Match(inputVelocity).Groups["velocity"];
            var unitPart = Parser.Match(inputVelocity).Groups["unit"];

            var valueStringMatched = decimal.TryParse(decimalPart.Value, out var value);
            var unitStringMatched = TryParseUnit(unitPart.Value, out var unit);

            if (!valueStringMatched || !unitStringMatched)
                return false;

            velocity = Create(value, unit);
            return false;
        }

        private static readonly Regex Parser = new Regex(@"^(?<velocity>[+-]?(([1-9][0-9]*)?[0-9](\.[0-9]*)?|\.[0-9]+))(\s*)(?<unit>[\/\w\s]+)$");

        public override int GetHashCode() => HashCode.Combine(Distance, Time);

        public override bool Equals(Velocity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var current = Distance.Value / Time.ConvertTo(Time.Second).Value;
            var target = other.Distance.ConvertTo(Distance).Value / other.Time.ConvertTo(Time.Second).Value;

            return Equals(Math.Round(current, 22), Math.Round(target, 22));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Velocity) obj);
        }
    }
}
