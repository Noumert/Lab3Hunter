namespace DefaultNamespace
{
    using UnityEngine;

    public abstract class DesiredVelocityProvider : MonoBehaviour
    {
        private float weight = 1f;
        
        public float Weight => weight;
        
        protected Wolf Wolf;

        private void Awake()
        {
            Wolf = GetComponent<Wolf>();
        }

        public abstract Vector3 GetDesiredVelocity();
    }
}