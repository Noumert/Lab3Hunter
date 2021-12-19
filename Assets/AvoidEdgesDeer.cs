using UnityEngine;

namespace DefaultNamespace
{
    public class AvoidEdgesDeer : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            var maxSpeed = Deer.MoveLimit;
            var v = Deer.Velocity;

            if (transform.position.x > 48)
            {
                return new Vector3(-maxSpeed, 0, 0);
            }

            if (transform.position.x < -48)
            {
                return new Vector3(maxSpeed, 0, 0);
            }

            if (transform.position.y > 28)
            {
                return new Vector3(0, -maxSpeed,0) ;
            }

            if (transform.position.y < -28)
            {
                return new Vector3(0, maxSpeed, 0);
            }

            return v;
        }
    }
}