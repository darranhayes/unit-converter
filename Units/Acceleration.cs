namespace Units
{
    /// <summary>
    /// Acceleration in meters per second per second (ms^-2, m/s^2)
    /// </summary>
    public class Acceleration
    {
        public static Velocity Accelerate(Meter metersPerSecondSquared, Second durationOfAccelerationInSeconds, Velocity initialVelocity)
        {
            var velocityInMs = initialVelocity.ConvertTo(Velocity.Ms);

            var newVelocityInMs = velocityInMs.Value + (metersPerSecondSquared.Value * durationOfAccelerationInSeconds.Value);

            return Velocity.Create(newVelocityInMs, Velocity.Ms).ConvertTo(initialVelocity.Unit);
        }
    }
}