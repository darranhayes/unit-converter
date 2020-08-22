using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Units
{
    public class Speed : IEquatable<Speed>
    {
        public static Speed Mph = new Speed(Distance.Mile, Time.Hour);
        public static Speed Kph = new Speed(Distance.Kilometer, Time.Hour);
        public static Speed Ms = new Speed(Distance.Meter, Time.Second);

        public static Speed Create(Distance distance, Time time)
        {
            return new Speed(distance, time.Unit);
        }

        public static Speed Create(decimal value, Speed unitSpeed)
        {
            var distance = Distance.Create(value, unitSpeed.Distance);
            return Create(distance, unitSpeed.Time);
        }

        public Distance Distance { get; }
        public Time Time { get; }

        private Speed(Distance distance, Time time)
        {
            Distance = distance;
            Time = time;
        }

        private static decimal GetConversionFactor(Speed s)
        {
            var distance = s.Distance.Unit.ConvertTo(Distance.Meter).Value;
            var time = s.Time.Unit.ConvertTo(Time.Second).Value;

            return distance / time;
        }

        public Speed ConvertTo(Speed target)
        {
            var currentFactor = GetConversionFactor(this);
            var targetFactor = GetConversionFactor(target);

            var targetDistance = Distance.Value * currentFactor / targetFactor;

            return new Speed(Distance.Create(targetDistance, target.Distance.Unit), target.Time.Unit);
        }

        public bool Equals(Speed other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var current = Distance.Value / Time.ConvertTo(Time.Second).Value;
            var target = other.Distance.ConvertTo(Distance).Value / other.Time.ConvertTo(Time.Second).Value;

            return Equals(current, target);
        }

        public override string ToString() => $"{Distance} / {Time}";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Speed) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Distance, Time);
        }
    }
}
