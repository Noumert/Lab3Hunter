namespace DefaultNamespace
{
    using UnityEngine;

    public class Aim : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            return new Vector3();
        }
    }
}