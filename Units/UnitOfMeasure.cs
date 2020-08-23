using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
    public abstract class UnitOfMeasure<T> : IEquatable<T> where T:UnitOfMeasure<T>
    {
        protected UnitOfMeasure(decimal value, string shortName, string longName, decimal valueInBaseUnit, params string[] aliases)
        {
            Value = value;
            ShortName = shortName;
            LongName = longName;
            ValueInBaseUnit = valueInBaseUnit;
            Aliases = aliases;
        }

        public decimal Value { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public string[] Aliases { get; }

        protected decimal ValueInBaseUnit { get; }

        public abstract T ConvertTo(T target);

        public abstract T Unit { get; }

        public override string ToString() => $"{Value:G29}{ShortName}";

        public string ToLongString() => $"{Value:G29}{ShortName} ({LongName})";

        public override int GetHashCode() => HashCode.Combine(ValueInBaseUnit);

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Math.Round(ValueInBaseUnit, 24), Math.Round(other.ValueInBaseUnit, 24));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((T)obj);
        }

    }
}
