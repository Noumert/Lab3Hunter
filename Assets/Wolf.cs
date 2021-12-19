using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wolf : MonoBehaviour
{
    private Vector3 velocity;

    private Vector3 acceleration;

    private float velocityLimit = 40;

    private float lifeTime = 10.0f;

    private float steeringForceLimit = 50;

    private const float Epsilon = 0.05f;

    public static float MoveLimit = 40;

    public Vector3 Velocity => velocity;

    public void ApplyForce(Vector3 force)
    {
        acceleration += force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponents<DeathI>().Length != 0 || other.gameObject.IsDestroyed() || other.gameObject.GetComponents<Wolf>().Length != 0 )
        {
            return;
        }

        if (other.gameObject.GetComponents<Player>().Length != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Destroy(other.gameObject);
        lifeTime += 5f;
    }

    private void Update()
    {
        ApplySteeringForce();

        ApplyForces();

        lifeTime -= Time.deltaTime;
        
        Debug.Log("Wolf life time left: " + lifeTime);

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        void ApplySteeringForce()
        {
            bool isAim = false;
            var providers = GetComponents<DesiredVelocityProvider>();
            var steering = Vector3.zero;
            foreach (var provider in providers)
            {
                Vector3 desiredVelocity;
                if (provider.GetType() == typeof(AimSeek) && !isAim &&
                    !provider.GetDesiredVelocity().Equals(Vector3.zero))
                {
                    isAim = true;
                    desiredVelocity = provider.GetDesiredVelocity() * provider.Weight; 
                    steering = desiredVelocity - velocity;
                }

                if (!isAim)
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

            transform.position += velocity * Time.deltaTime;
            acceleration = Vector3.zero;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 lookDir = velocity;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}