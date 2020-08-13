using System;

namespace Units
{
    public class Distance
    {
        public static Distance Create(decimal distance, Length length) => new Distance(distance, length);

        private Distance(decimal distance, Length length)
        {
            Value = distance;
            Unit = length;
            ValueInMeters = Value * Unit.Ratio;
        }

        public Distance ConvertTo(Length target)
        {
            var targetDistance = ValueInMeters / target.Ratio;

            return new Distance(targetDistance, target);
        }

        public decimal Value { get; }
        public Length Unit { get; }
        private decimal ValueInMeters { get; }

        public override string ToString() => $"{Math.Round(Value, 2)}{Unit}";
        public string ToLongString() => $"{Value}{Unit.ToLongString()}";

        public override bool Equals(object obj)
        {
            var unit = obj as Distance;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Distance other) => ValueInMeters == other.ValueInMeters;

        public override int GetHashCode() => ValueInMeters.GetHashCode();
    }
}