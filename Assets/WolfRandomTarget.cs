using UnityEngine;

namespace DefaultNamespace
{
    public class WolfRandomTarget : DesiredVelocityProvider
    {
        private Vector3 moveAim;
        
        private void Start()
        {
            InvokeRepeating("randomAim", 0, 4);
        }

        void randomAim()
        {
            int x = Random.Range(-52, 52);
            int y = Random.Range(-32, 32);
            moveAim = new Vector3(x, y);
        }

        public override Vector3 GetDesiredVelocity()
        {
            // int x = Random.Range(-30,39);
            // int y = Random.Range(-29,27);
            // return -(transform.position - new Vector3(x,y)).normalized * RabbitMove.VelocityLimit;
            return -(transform.position - moveAim).normalized * Wolf.MoveLimit;
        }
    }
}