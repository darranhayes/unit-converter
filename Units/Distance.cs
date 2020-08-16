using System;

namespace Units
{
    public class Distance
    {
        public static Distance Create(decimal distance, Length length) => new Distance(distance, length);

        private Distance(decimal distance, Length lengthUnit)
        {
            ActualLength = Length.Create(distance, lengthUnit);
        }

        public Distance ConvertTo(Length target)
        {
            var targetLength = ActualLength.ConvertTo(target);
            return new Distance(targetLength.Value, target);
        }

        public decimal Value => ActualLength.Value;

        private Length ActualLength { get; }

        public override string ToString() => ActualLength.ToString();
        public string ToLongString() => ActualLength.ToLongString();

        public override bool Equals(object obj)
        {
            var unit = obj as Distance;
            if (unit == null)
                return false;

            return Equals(unit);
        }

        protected bool Equals(Distance other) => ActualLength.Equals(other.ActualLength);

        public override int GetHashCode() => ActualLength.GetHashCode();
    }
}