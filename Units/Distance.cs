using System;

namespace Units
{
    public class Distance
    {
        public static Distance Create(decimal distance, UnitLength unitLength)
        {
            return new Distance(distance, unitLength);
        }

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

        public override string ToString()
        {
            return $"{Math.Round(Value, 2)}{Unit}";
        }

        public string ToLongString()
        {
            return $"{Value}{Unit.ToLongString()}";
        }
    }
}