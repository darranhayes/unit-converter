using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
    public abstract class UnitOfMeasure<T> : IEquatable<T>
    {
        protected UnitOfMeasure(decimal value, string shortName, string longName, params string[] aliases)
        {
            Value = value;
            ShortName = shortName;
            LongName = longName;
            Aliases = aliases;
        }

        public decimal Value { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public string[] Aliases { get; }

        public abstract T ConvertTo(T target);

        public abstract T Unit { get; }

        public abstract string ToLongString();

        public abstract bool Equals(T other);

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();
    }
}
