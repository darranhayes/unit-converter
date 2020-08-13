using System;

namespace Units
{
    public class Duration
    {
        public static Duration Create(decimal duration, Time time) => new Duration(duration, time);

        private Duration(decimal distance, Time length)
        {
            Value = distance;
            Unit = length;
            ValueInSeconds = Value * Unit.Ratio;
        }

        public Duration ConvertTo(Time target)
        {
            var targetDistance = ValueInSeconds / target.Ratio;

            return new Duration(targetDistance, target);
        }

        public decimal Value { get; }
        public Time Unit { get; }
        private decimal ValueInSeconds { get; }

        public override string ToString() => $"{Math.Round(Value, 2)}{Unit}";
        public string ToLongString() => $"{Value}{Unit.ToLongString()}";

        public override bool Equals(object obj)
        {
            var unit = obj as Duration;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Duration other) => ValueInSeconds == other.ValueInSeconds;

        public override int GetHashCode() => ValueInSeconds.GetHashCode();
    }
}