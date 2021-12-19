

using UnityEngine;

namespace DefaultNamespace
{
    public class AvoidEdgesWolf : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            var maxSpeed = Rabbit.RunLimit;
            var v = Rabbit.Velocity;

            if (transform.position.x > 50)
            {
                return new Vector3(-maxSpeed, 0, 0);
            }

            if (transform.position.x < -50)
            {
                return new Vector3(maxSpeed, 0, 0);
            }

            if (transform.position.y > 30)
            {
                return new Vector3(0, -maxSpeed,0) ;
            }

            if (transform.position.y < -30)
            {
                return new Vector3(0, maxSpeed, 0);
            }

            return v;
        }
    }
}