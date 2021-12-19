using UnityEngine;

namespace DefaultNamespace
{
    public class Deer : MonoBehaviour
    {
        private Vector3 velocity;

        private Vector3 acceleration;

        private float velocityLimit = 30;

        private float steeringForceLimit = 60;

        private const float Epsilon = 0.05f;

        public static float MoveLimit = 30;

        public static Vector3 Velocity = new Vector3();

        public void ApplyForce(Vector3 force)
        {
            acceleration += force;
        }


        private void Update()
        {
            ApplySteeringForce();

            ApplyForces();

            Velocity = velocity;

            void ApplySteeringForce()
            {
                bool isRun = false;
                var providers = GetComponents<DesiredVelocityProvider>();
                var steering = Vector3.zero;
                foreach (var provider in providers)
                {
                    Vector3 desiredVelocity;
                    if (provider.GetType() == typeof(DeerRun) && !isRun &&
                        !provider.GetDesiredVelocity().Equals(Vector3.zero))
                    {
                        desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;
                        if (desiredVelocity == Vector3.zero)
                        {
                            desiredVelocity = velocity;
                        }

                        steering += desiredVelocity - velocity;
                    }

                    if (provider.GetType() == typeof(AvoidEdgesDeer))
                    {
                        desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;

                        steering += desiredVelocity - velocity;
                    }

                    if (!isRun)
                    {
                        
                        desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;

                        steering += desiredVelocity - velocity;
                    }
                }


                ApplyForce(Vector3.ClampMagnitude(steering - velocity, steeringForceLimit));
            }

            void ApplyForces()
            {
                velocity += acceleration * Time.deltaTime;
                //limit velocity
                // velocity = ClampMagnitude(velocity, velocityLimit, velocityMinLimit);
                velocity = Vector3.ClampMagnitude(velocity, velocityLimit);

                if (velocity.magnitude < Epsilon)
                {
                    velocity = Vector3.zero;
                    return;
                }

                velocity.z = 0;

                transform.position += velocity * Time.deltaTime;
                acceleration = Vector3.zero;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                Vector2 lookDir = velocity;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }
        }
    }
}