using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeersRandomTarget : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            Vector3 moveAim = Vector3.zero;
            var deerGroups = Spawn.deerGroups;
            foreach (var dGroup in deerGroups)
            {
                if (dGroup.Contains(gameObject))
                {
                    int index = deerGroups.IndexOf(dGroup);
                    moveAim = RandomAimsForAllDeerGroups.moveAims[index];
                }
            }
            
            
            return -(transform.position - moveAim).normalized * Deer.MoveLimit;
        }
    }
}