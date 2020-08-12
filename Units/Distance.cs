using System;

namespace Units
{
    public class Distance
    {
        public static Distance Create(decimal distance, UnitLength unitLength) => new Distance(distance, unitLength);

        private Distance(decimal distance, UnitLength unitLength)
        {
            Value = distance;
            Unit = unitLength;
        }

        public Distance ConvertTo(UnitLength targetUnit)
        {
            var targetDistance = (Value * Unit.Ratio) / targetUnit.Ratio;

            return new Distance(targetDistance, targetUnit);
        }

        public decimal Value { get; }
        public UnitLength Unit { get; }

        public override string ToString() => $"{Math.Round(Value, 2)}{Unit}";

        public string ToLongString() => $"{Value}{Unit.ToLongString()}";

        public override bool Equals(object obj)
        {
            var unit = obj as Distance;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Distance other) => Value * Unit.Ratio == other.Value * other.Unit.Ratio;

        public override int GetHashCode() => HashCode.Combine(Value, Unit);
    }
}