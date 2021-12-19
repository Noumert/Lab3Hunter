using UnityEngine;

namespace DefaultNamespace
{
    public class RabbitRun : DesiredVelocityProvider
    {
        public int senseRange = 5;
        private Collider2D nearestCollider2D;

        private void Update()
        {
            nearestCollider2D = null;
            Collider2D[] collidersArray = Physics2D
                .OverlapCircleAll(transform.position, senseRange);
            if (collidersArray.Length == 0)
            {
                return;
            }

            foreach (Collider2D collider2D in collidersArray)
            {
                var distance = Vector3.Distance(collider2D.transform.position, transform.position);
                if (distance != 0 && collider2D.GetComponent<DeathI>() == null)
                {
                    nearestCollider2D = collider2D;
                }
            }

            foreach (Collider2D collider2D in collidersArray)
            {
                if (nearestCollider2D == null)
                {
                    return;
                }

                var nearestDistance = Vector3.Distance(nearestCollider2D.transform.position, transform.position);
                var distance = Vector3.Distance(collider2D.transform.position, transform.position);
                if (nearestDistance > distance && distance != 0 && collider2D.GetComponent<DeathI>() == null)
                {
                    nearestCollider2D = collider2D;
                }
            }
        }

        public override Vector3 GetDesiredVelocity()
        {
            if (nearestCollider2D == null)
            {
                return Vector3.zero;
            }

            return (transform.position - nearestCollider2D.transform.position).normalized * Rabbit.RunLimit;
        }
    }
}