using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeerRun : DesiredVelocityProvider
    {
        public int senseRange = 30;
        private Collider2D nearestCollider2D;

        private void Update()
        {
            nearestCollider2D = null;
            Collider2D[] collidersArray = Physics2D
                .OverlapCircleAll(transform.position, senseRange);
            foreach (Collider2D collider2D in collidersArray)
            {
                if (collider2D.GetComponent<Deer>() == null && collider2D.GetComponent<DeathI>() == null)
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
                var nearestDistance = Vector3.Distance(nearestCollider2D.transform.position,transform.position);
                var distance = Vector3.Distance(collider2D.transform.position,transform.position);
                if ((nearestDistance > distance || nearestDistance == 0 && distance != 0) 
                    && collider2D.GetComponent<Deer>() == null 
                    && collider2D.GetComponent<Rabbit>() == null 
                    && collider2D.GetComponent<DeathI>() == null)
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
            return (transform.position - nearestCollider2D.transform.position).normalized * Deer.MoveLimit;
        }
    }
}