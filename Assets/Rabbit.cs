using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Rabbit : MonoBehaviour
    {
        private Vector3 velocity;

        private Vector3 acceleration;

        private float velocityLimit = 70;

        private float steeringForceLimit = 120;

        private const float Epsilon = 0.05f;

        public static float RunLimit = 70;

        public static float MoveLimit = 15;

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
                bool isEdge = false;
                var providers = GetComponents<DesiredVelocityProvider>();
                var steering = Vector3.zero;
                foreach (var provider in providers)
                {
                    Vector3 desiredVelocity;
                    desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;
                    if (desiredVelocity == Vector3.zero)
                    {
                        desiredVelocity = velocity;
                    }
                    steering += desiredVelocity - velocity;
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