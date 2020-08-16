using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
    public class Speed : IEquatable<Speed>
    {
        public static Speed Create(Distance distance, Time time)
        {
            if (!time.IsUnit())
                throw new ArgumentException("Time must be a unit value of time", nameof(time));

            return new Speed(distance, time);
        }

        public Distance Distance { get; }

        public Time Time { get; }

        private Speed(Distance distance, Time time)
        {
            Distance = distance;
            Time = time;
        }

        public Distance GetDistanceAfter(Time travelingFor)
        {
            var duration = travelingFor.ConvertTo(Time);
            var distanceTraveled = Distance.Value * duration.Value;

            return Distance.Create(distanceTraveled, Distance);
        }

        public Time GetTimeAfter(Distance travelingFor)
        {
            var distance = travelingFor.ConvertTo(Distance);
            var timeTaken = distance.Value / Distance.Value;

            return Time.Create(timeTaken, Time);
        }

        public bool Equals(Speed other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var current = Distance.Value / Time.ConvertTo(Time.Second).Value;
            var target = other.Distance.ConvertTo(Distance).Value / other.Time.ConvertTo(Time.Second).Value;

            return Equals(current, target);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Speed) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Distance, Time);
        }
    }
}
