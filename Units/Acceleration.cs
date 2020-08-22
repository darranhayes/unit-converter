namespace Units
{
    public class Acceleration
    {
        public static Acceleration Create(Speed speedChangePerUnitOfTime) => new Acceleration(speedChangePerUnitOfTime);
        public static Acceleration Create(decimal metersPerSecond) => new Acceleration(Speed.Create(metersPerSecond, Speed.Ms));

        private Acceleration(Speed speedChangePerUnitOfTime)
        {
            _velocityChangePerSecond = speedChangePerUnitOfTime.ConvertTo(Speed.Ms);
        }

        private readonly Speed _velocityChangePerSecond;

        public Speed Accelerate(Speed speed, Time duration)
        {
            var durationInSeconds = duration.ConvertTo(Time.Second);

            var speedInMs = speed.ConvertTo(Speed.Ms);

            var newSpeedInMs = speedInMs.Value + (_velocityChangePerSecond.Value * durationInSeconds.Value);

            return Speed.Create(newSpeedInMs, Speed.Ms).ConvertTo(speed.Unit);
        }
    }
}